$(document).ready(function ()
{
    $("#show-button").click(function ()
    {
        var accountId = document.getElementById("accounts").value;
        $('#accountInfo').load("/Account/AccountInfo/" + accountId);
    }
    );
}
);
