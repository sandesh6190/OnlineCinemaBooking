var options = {
	series: [32500, 29700, 24200],
	chart: {
		height: 177,
		type: "polarArea",
	},
	labels: ["USA", "India", "Brazil"],
	fill: {
		opacity: 1,
	},
	stroke: {
		width: 1,
		colors: ["#0a50d8", "#57637B", "#D6DAE3"],
	},
	colors: ["#eaf1ff", "#e2e5ec", "#eff1f6"],
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
