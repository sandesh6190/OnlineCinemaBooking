var options = {
	chart: {
		height: 157,
		type: "bar",
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		bar: {
			horizontal: false,
			columnWidth: "40px",
			borderRadius: 7,
		},
	},
	dataLabels: {
		enabled: false,
	},
	stroke: {
		show: true,
		width: 1,
		colors: ["#0a50d8", "#57637B", "#D6DAE3"],
	},
	series: [
		{
			name: "New",
			data: [2000, 5500, 4900, 6000, 2000, 6000, 2000],
		},
		{
			name: "Returning",
			data: [2500, 3500, 6500, 3500, 4500, 3000, 8500],
		},
	],
	legend: {
		show: false,
	},
	xaxis: {
		categories: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
	},
	yaxis: {
		show: false,
	},
	fill: {
		opacity: 1,
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return "$ " + val + " thousands";
			},
		},
	},
	grid: {
		borderColor: "#ccd2da",
		strokeDashArray: 3,
		xaxis: {
			lines: {
				show: true,
			},
		},
		yaxis: {
			lines: {
				show: false,
			},
		},
		padding: {
			top: 0,
			right: 0,
			bottom: 0,
			left: 0,
		},
	},
	colors: ["#eaf1ff", "#e2e5ec"],
};
var chart = new ApexCharts(document.querySelector("#revenueData"), options);
chart.render();
