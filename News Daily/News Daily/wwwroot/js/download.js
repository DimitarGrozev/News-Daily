$(function x() {
	var myArr = [];
	$(document).on('click', '.download-btn', function (x) {
		x.preventDefault();
		var topics = $('.topic').last().val();
		console.log(topics);
		var obj = { "SearchString": $('#searchString').val(), "SortString": $('#sort-string').val(), "LanguageString": $('#language').val(), "Topic": topics };
		var articles = JSON.stringify(obj);
		console.log(articles);
		$.ajax({
			url: '/News/Download/',
			type: "POST",
			xhrFields: {
				responseType: 'blob'
			},
			data: { 'articles': articles },
			success: function (data) {
				var a = document.createElement('a');
				var url = window.URL.createObjectURL(data);
				a.href = url;
				a.download = 'articles.xlsx';
				document.body.append(a);
				a.click();
				a.remove();
				window.URL.revokeObjectURL(url);
			},

		})
	});
});