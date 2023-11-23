// Markers on the world map
$(function () {
	$("#world-map-markers").vectorMap({
		map: "world_mill_en",
		normalizeFunction: "polynomial",
		hoverOpacity: 0.7,
		hoverColor: false,
		zoomOnScroll: false,
		markerStyle: {
			initial: {
				fill: "#53810c",
				stroke: "#53810c",
				r: 6,
			},
		},
		zoomMin: 1,
		hoverColor: true,
		series: {
			regions: [
				{
					values: gdpData,
					scale: ["#53810c", "#a5b3c4", "#bccada", "#e6ecf3"],
					normalizeFunction: "polynomial",
				},
			],
		},
		backgroundColor: "transparent",
	});
});
