$(function () {

	function SearchBtn() {
		page = 1;
		var language = $('#language').val();
		var searchString = $('#searchString').val();
		var sortString = $('#sort-string').val();
		$.ajax({
			url: '/News/Index/',
			type: "GET",

			data: {
				'SearchString': searchString,
				'LanguageString': language,
				'SortString': sortString,
				"IsAjax": true
			},
			success: function (data) {
				var urlToImageCollection = document.getElementsByClassName("takeUrlToImage");
				var urlCollection = document.getElementsByClassName("takeURL");
				var titleCollection = document.getElementsByClassName("takeTitle");
				var descCollection = document.getElementsByClassName("takeDescription");
				var publishedAtCollection = document.getElementsByClassName("takePublishedAt");
				console.log(titleCollection);
				$('#replace-container').replaceWith(data);
				changeArr(urlToImageCollection, urlCollection, titleCollection, descCollection, publishedAtCollection);
				$('#prev-btn').css("display", "none");

			},

		});
	}
	function changeArr(urlToImageCollection, urlCollection, titleCollection, descCollection, publishedAtCollection) {
		myArr = [];
		for (let i = 0; i < 40; i++) {
			myArr.push(makeObject(titleCollection.item(i).value, descCollection.item(i).value, urlCollection.item(i).value, publishedAtCollection.item(i).value, urlToImageCollection.item(i).value));
		}
		console.log(myArr);
	}
	$('#search-btn').click(() => {
		SearchBtn();
	})
	document.onkeyup = function (e) {
		if (e.which == 13) {
			SearchBtn();
		}
	}
});