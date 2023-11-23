// Morris Donut
Morris.Donut({
	element: "donutColors",
	data: [
		{ value: 30, label: "foo" },
		{ value: 15, label: "bar" },
		{ value: 10, label: "baz" },
		{ value: 5, label: "A really really long label" },
	],
	labelColor: "#507D0C",
	colors: ["#507D0C", "#719D2C", "#90BA4C", "#C4D3AC", "E3EBD5"],
	resize: true,
	hideHover: "auto",
	gridLineColor: "#ccd2da",
	formatter: function (x) {
		return x + "%";
	},
});
