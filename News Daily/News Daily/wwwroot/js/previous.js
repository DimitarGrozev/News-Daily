$(function () {
	$(document).on('click', '#prev-btn', function (x) {

		page -= 1;
		if (page == 1) {
			$('#prev-btn').css("display", "none");
		}
		if (page < 8) {
			$('#next-btn').css("display", "block");
		}
		var div = $('#replace-container')
		var htmlToRplaceWith = `<div class="container" id="replace-container">
		<div class="row">`
		for (let i = (page - 1) * 5; i < (page - 1) * 5 + 5; i++) {
			htmlToRplaceWith += `
				<div class="d-flex col-12 rp-card">
					<div class="col-4 rp-img-container">
						<div class="rp-img" style="background-image:url('${myArr[i].UrlToImage}');">
						</div>
					</div>
					<div class="d-flex flex-column col-6">
						<a class="rp-title" href="${myArr[i].Url}" target="_blank">${myArr[i].Title}</a>
						<div class="pb-2 d-flex justify-content-between">

							<span class="rp-date">${myArr[i].PublishedAt}</span>
						</div>
						<q class="rp-quote">${myArr[i].Description}</q> <br>
					</div>

				</div>`
		}

		htmlToRplaceWith += `</div>
	</div>`
		div.replaceWith(htmlToRplaceWith);
	});



});