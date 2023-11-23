var options = {
	series: [32500, 29700, 24200],
	chart: {
		height: 270,
		type: "polarArea",
	},
	labels: ["USA", "India", "Brazil"],
	fill: {
		opacity: 1,
	},
	stroke: {
		width: 1,
		colors: ["#78590e", "#78590e", "#78590e"],
	},
	colors: ["#f2e7ce", "#c59421", "#78590e"],
	yaxis: {
		show: false,
	},
	legend: {
		show: false,
	},
	tooltip: {
		y: {
			formatter: function (val) {
				return val;
			},
		},
	},
	plotOptions: {
		polarArea: {
			rings: {
				strokeWidth: 0,
			},
			spokes: {
				strokeWidth: 0,
			},
		},
	},
};

var chart = new ApexCharts(document.querySelector("#paymentsData"), options);
chart.render();
