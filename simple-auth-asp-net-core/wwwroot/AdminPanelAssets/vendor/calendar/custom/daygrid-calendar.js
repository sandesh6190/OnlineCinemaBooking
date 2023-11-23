document.addEventListener("DOMContentLoaded", function () {
	var calendarEl = document.getElementById("dayGrid");
	var calendar = new FullCalendar.Calendar(calendarEl, {
		headerToolbar: {
			left: "prevYear,prev,next,nextYear today",
			center: "title",
			right: "dayGridMonth,dayGridWeek,dayGridDay",
		},
		initialDate: "2022-10-12",
		navLinks: true, // can click day/week names to navigate views
		editable: true,
		dayMaxEvents: true, // allow "more" link when too many events
		events: [
			{
				title: "All Day Event",
				start: "2022-10-01",
				color: "#e6f5e3",
				borderColor: "#41ab2e",
				textColor: "#41ab2e",
			},
			{
				title: "Long Event",
				start: "2022-10-07",
				end: "2022-10-10",
				color: "#eaf1ff",
				borderColor: "#0a50d8",
				textColor: "#0a50d8",
			},
			{
				groupId: 999,
				title: "Birthday",
				start: "2022-10-09T16:00:00",
				color: "#e2e5ec",
				borderColor: "#434957",
				textColor: "#434957",
			},
			{
				groupId: 999,
				title: "Birthday",
				start: "2022-10-16T16:00:00",
				color: "#ffebeb",
				borderColor: "#ce385b",
				textColor: "#ce385b",
			},
			{
				title: "Conference",
				start: "2022-10-11",
				end: "2022-10-13",
				color: "#e8f4d6",
				borderColor: "#53810c",
				textColor: "#53810c",
			},
			{
				title: "Meeting",
				start: "2022-10-14T10:30:00",
				end: "2022-10-14T12:30:00",
				color: "#e8f4ff",
				borderColor: "#0b5aa9",
				textColor: "#0b5aa9",
			},
			{
				title: "Lunch",
				start: "2022-10-16T12:00:00",
				color: "#fff5d5",
				borderColor: "#d49d1c",
				textColor: "#d49d1c",
			},
			{
				title: "Meeting",
				start: "2022-10-18T14:30:00",
				color: "#e2ddff",
				borderColor: "#7164b5",
				textColor: "#7164b5",
			},
			{
				title: "Interview",
				start: "2022-10-21T17:30:00",
				color: "#ffe6fe",
				borderColor: "#780974",
				textColor: "#780974",
			},
			{
				title: "Meeting",
				start: "2022-10-22T20:00:00",
				color: "#ffeade",
				borderColor: "#d45c1c",
				textColor: "#d45c1c",
			},
			{
				title: "Birthday",
				start: "2022-10-13T07:00:00",
				color: "#dde3c9",
				borderColor: "#93b711",
				textColor: "#93b711",
			},
			{
				title: "Click for Google",
				url: "https://bootstrap.gallery/",
				start: "2022-10-28",
				color: "#e4e7ed",
				borderColor: "#294e9d",
				textColor: "#294e9d",
			},
			{
				title: "Interview",
				start: "2022-10-20",
				color: "#ece0d9",
				borderColor: "#c55513",
				textColor: "#c55513",
			},
			{
				title: "Product Launch",
				start: "2022-10-29",
				color: "#eedee6",
				borderColor: "#b32268",
				textColor: "#b32268",
			},
			{
				title: "Leave",
				start: "2022-10-25",
				color: "#dfffe1",
				borderColor: "#1c8b24",
				textColor: "#1c8b24",
			},
		],
	});

	calendar.render();
});
