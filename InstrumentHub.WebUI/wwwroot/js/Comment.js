CommentBodyId = "#comment"
var productId = -1
function imageBox(smallImg) {
    var fullImg = document.getElementById("image-box")
    fullImg.src = smallImg.src
}

$(document).ready(function () {
    var url = $("#comment").data("url")
    $("#comment").load(url)
    productId = $("#comment").data("product-id")
    $(CommentBodyId).load("/Comment/ShowProductComments?id=" + productId)
})

function doComment(btn, e, commentId, spanId) {
    var button = $(btn)

    if (e == 'new_clicked') {
        var txt = $("#new_comment_text").val()

        $.ajax({
            method: "POST",
            url: '/Comment/Create',
            data: { 'text': txt, 'productId': productId }
        }).done(function (data) {
            if (data.result) {
                $(CommentBodyId).load("/Comment/ShowProductComments?id=" + productId)
            }
            else {
                alert("Yorum yapılırken bir hata oluştu!")
            }
        }).fail(function (error) {
            alert("Sunucuda bir hata oluştu!")
        })
    }
    else if (e == 'delete_clicked') {
        var dialog_res = confirm("Yorum Silinsin mi?")

        if (!dialog_res) return false

        $.ajax({
            method: "POST",
            url: '/Comment/Delete?id=' + commentId,
        }).done(function (data) {
            if (data.result) {
                $(CommentBodyId).load("/Comment/ShowProductComments?id=" + productId)
            }
            else {
                alert("Yorum Silinemedi!")
            }
        }).fail(function (error) {
            alert("Sunucuda bir hata oluştu!")
        })
    }
   
}