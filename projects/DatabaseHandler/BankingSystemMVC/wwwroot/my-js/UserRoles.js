newValuesAdded = [];
newValuesRemoved = [];

$('#my-select').multiSelect(
    {
    selectableHeader: "<div class='custom-header'>Unassigned roles</div>",
    selectionHeader: "<div class='custom-header'>Assigned roles</div>",

        afterSelect: function (value)
        {
        if (newValuesRemoved.includes(value[0])) 
        {
            const index = newValuesRemoved.indexOf(value[0]);
            newValuesRemoved.splice(index, 1);
        }
        else
        {
            newValuesAdded.push(value[0]);
        }
    },
        afterDeselect: function (value)
        {
        if (newValuesAdded.includes(value[0])) 
        {
            const index = newValuesAdded.indexOf(value[0]);
            newValuesAdded.splice(index, 1);
        }
        else 
        {
            newValuesRemoved.push(value[0]);
        }
    }
    }
);


$(document).ready(function() 
{
    var userId = $('#userId').text();
    $('#saveRoles').click(function() 
    {
        var objectToSend =
        {
            userId: userId,
            valuesAdded: newValuesAdded,
            valuesRemoved: newValuesRemoved
        };

        $.ajax({
            type: "POST",
            url: "/Admin/UserRoles",
            data: { roles: JSON.stringify(objectToSend) },
            success: function (msg)
            {
                if (msg) 
                {
                    console.log("Roles updated !");
                } 
                else 
                {
                    console.log("Something went wrong!");
                }   
            },
            error: function (xhr, ajaxOptions, thrownError)
            {
                console.log(ajaxOptions);
                console.log(JSON.stringify(objectToSend));
                console.log(xhr.status);
                console.log(thrownError);
            }
            
        }
        )
        alert("Roles updated!");
    }
    );

}
);



