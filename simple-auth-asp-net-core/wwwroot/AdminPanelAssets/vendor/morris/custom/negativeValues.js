// Morris Negative values
var neg_data = [
	{ period: "2022-02-12", a: 100 },
	{ period: "2022-01-03", a: 75 },
	{ period: "2021-08-08", a: 50 },
	{ period: "2021-05-10", a: 25 },
	{ period: "2021-03-14", a: 0 },
	{ period: "2021-01-10", a: -25 },
	{ period: "2020-12-10", a: -50 },
	{ period: "2020-10-07", a: -75 },
	{ period: "2020-09-25", a: -100 },
];
Morris.Line({
	element: "negativeValues",
	data: neg_data,
	xkey: "period",
	ykeys: ["a"],
	labels: ["Series A"],
	units: "%",
	resize: true,
	hideHover: "auto",
	gridLineColor: "#ccd2da",
	pointFillColors: ["#ffffff"],
	pointStrokeColors: ["#507D0C", "#719D2C", "#90BA4C", "#C4D3AC", "E3EBD5"],
	lineColors: ["#507D0C", "#719D2C", "#90BA4C", "#C4D3AC", "E3EBD5"],
});
