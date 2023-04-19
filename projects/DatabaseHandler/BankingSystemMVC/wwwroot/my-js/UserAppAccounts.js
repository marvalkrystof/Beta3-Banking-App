$(document).ready(function ()
{
    $("#show-button").click(function ()
    {
        var accountId = document.getElementById("accounts").value;
        $('#roleInfo').load("/Admin/UpdateUserAccount/" + accountId);
    }
    );
}
);
