$(document).ready(function ()
{
	$("#show-button").click(function ()
	{
		var employeeId = document.getElementById("employees").value;
		$('#employeeInfo').load("/User/ShowEmployeeInfo/" + employeeId);
	}
	);
}
);