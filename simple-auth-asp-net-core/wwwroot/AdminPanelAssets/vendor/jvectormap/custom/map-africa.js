// Africa
$(function () {
	$("#mapAfrica").vectorMap({
		map: "africa_mill",
		backgroundColor: "transparent",
		scaleColors: ["#0a50d8", "#333333"],
		zoomOnScroll: false,
		zoomMin: 1,
		hoverColor: true,
		series: {
			regions: [
				{
					values: gdpData,
					scale: ["#0a50d8", "#333333"],
					normalizeFunction: "polynomial",
				},
			],
		},
	});
});
