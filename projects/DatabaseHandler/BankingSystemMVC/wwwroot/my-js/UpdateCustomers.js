$(document).ready(function ()
{
	$("#show-button").click(function ()
	{
		var customerId = document.getElementById("customers").value;
		$('#customerInfo').load("/User/UpdateCustomer/" + customerId);
	}
	);
}
);