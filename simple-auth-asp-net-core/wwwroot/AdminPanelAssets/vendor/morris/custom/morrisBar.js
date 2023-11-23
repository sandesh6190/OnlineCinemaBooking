// Morris Bar Chart
Morris.Bar({
	element: "morrisBar",
	data: [
		{ x: "2022 Q1", y: 2, z: 4, a: 2 },
		{ x: "2022 Q2", y: 5, z: 3, a: 1 },
		{ x: "2022 Q3", y: 2, z: 7, a: 4 },
		{ x: "2022 Q4", y: 5, z: 6, a: 3 },
	],
	xkey: "x",
	ykeys: ["y", "z", "a"],
	labels: ["Y", "Z", "A"],
	resize: true,
	hideHover: "auto",
	gridLineColor: "#ccd2da",
	barColors: ["#507D0C", "#719D2C", "#90BA4C", "#C4D3AC", "E3EBD5"],
});
