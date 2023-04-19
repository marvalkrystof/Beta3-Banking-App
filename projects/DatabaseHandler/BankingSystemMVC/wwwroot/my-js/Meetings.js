$(document).ready(function ()
{
	$("#show-button").click(function ()
	{
		var meetingId = document.getElementById("meetings").value;
		$('#meetingInfo').load("/Meeting/MeetingInfo/" +meetingId);
	}
	);
}
);
