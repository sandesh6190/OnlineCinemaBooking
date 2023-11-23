// Europe
$(function () {
	$("#mapEurope").vectorMap({
		map: "europe_mill",
		zoomOnScroll: false,
		series: {
			regions: [
				{
					values: gdpData,
					scale: ["#0a50d8", "#333333"],
					normalizeFunction: "polynomial",
				},
			],
		},
		backgroundColor: "transparent",
	});
});
