var options = {
	chart: {
		height: 160,
		type: "radialBar",
		toolbar: {
			show: false,
		},
	},
	plotOptions: {
		radialBar: {
			dataLabels: {
				total: {
					show: true,
					label: "Total",
					formatter: function (w) {
						// By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
						return "96";
					},
				},
			},
			track: {
				background: "#e6ecf3",
			},
		},
	},
	series: [80, 70, 20],
	labels: ["New", "Pending", "Delivered"],
	colors: ["#0a50d8", "#57637B", "#D6DAE3"],
};

var chart = new ApexCharts(document.querySelector("#ordersData"), options);
chart.render();
