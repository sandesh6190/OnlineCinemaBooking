document.addEventListener("DOMContentLoaded", function () {
	/* initialize the external events
    -----------------------------------------------------------------*/

	var containerEl = document.getElementById("external-events-list");
	new FullCalendar.Draggable(containerEl, {
		itemSelector: ".fc-event",
		eventData: function (eventEl) {
			return {
				title: eventEl.innerText.trim(),
			};
		},
	});

	//// the individual way to do it
	// var containerEl = document.getElementById('external-events-list');
	// var eventEls = Array.prototype.slice.call(
	//   containerEl.querySelectorAll('.fc-event')
	// );
	// eventEls.forEach(function(eventEl) {
	//   new FullCalendar.Draggable(eventEl, {
	//     eventData: {
	//       title: eventEl.innerText.trim(),
	//     }
	//   });
	// });

	/* initialize the calendar
    -----------------------------------------------------------------*/

	var calendarEl = document.getElementById("draggableCalendar");
	var calendar = new FullCalendar.Calendar(calendarEl, {
		headerToolbar: {
			left: "prev,next today",
			center: "title",
			right: "dayGridMonth,timeGridWeek,timeGridDay,listWeek",
		},
		initialDate: "2022-10-10",
		editable: true,
		droppable: true, // this allows things to be dropped onto the calendar
		drop: function (arg) {
			// is the "remove after drop" checkbox checked?
			if (document.getElementById("drop-remove").checked) {
				// if so, remove the element from the "Draggable Events" list
				arg.draggedEl.parentNode.removeChild(arg.draggedEl);
			}
		},
		events: [
			{
				title: "Team Meeting",
				start: "2022-10-04",
				color: "#e6f5e3",
				borderColor: "#41ab2e",
				textColor: "#41ab2e",
			},
			{
				title: "Training",
				start: "2022-10-07",
				end: "2022-10-08",
				color: "#eaf1ff",
				borderColor: "#0a50d8",
				textColor: "#0a50d8",
			},
			{
				groupId: 999,
				title: "Birthday",
				start: "2022-10-09",
				color: "#e2e5ec",
				borderColor: "#434957",
				textColor: "#434957",
			},
			{
				title: "Interview",
				start: "2022-10-12",
				color: "#ffebeb",
				borderColor: "#ce385b",
				textColor: "#ce385b",
			},
			{
				title: "Travel to Japan",
				start: "2022-10-14T10:30:00",
				color: "#e8f4d6",
				borderColor: "#53810c",
				textColor: "#53810c",
			},
			{
				title: "Interview",
				start: "2022-10-21T17:30:00",
				color: "#e8f4ff",
				borderColor: "#0b5aa9",
				textColor: "#0b5aa9",
			},
			{
				title: "Birthday",
				start: "2022-10-25T19:00:00",
				color: "#fff5d5",
				borderColor: "#d49d1c",
				textColor: "#d49d1c",
			},
			{
				title: "Leave",
				start: "2022-10-28",
				color: "#e2ddff",
				borderColor: "#7164b5",
				textColor: "#7164b5",
			},
		],
	});
	calendar.render();
});
