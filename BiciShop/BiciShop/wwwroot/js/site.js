// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#biciListDiv').load(`/Home/BiciList/?firstLoad=true`)
$('#orderList').load(`/Order/OrderList/?firstLoad=true`)


$('.purchaseBtn').hover(function () {
    $(this).children('.purchase').attr('src', '/Files/purchase_white.png');
})

$('.purchaseBtn').mouseleave(function () {
    if (!$(this).hasClass('clicked')) {
        $(this).children('.purchase').attr('src', '/Files/purchase_black.png');
    }
})

$('#riseDelete').click(function () {
    $('#deletePanel').fadeToggle()
})
$('#canelBtn').click(function () {
    $('#deletePanel').fadeToggle()
})

$('#showFilter').click(function () {
    $('#blur').css('display', 'block')
    $('#filterForm').css('display', 'block')
})
$('#close').click(function () {
    $('#blur').css('display', 'none')
    $('#filterForm').css('display', 'none')
})



$('#inputGroupFile01').change(function () {
    var input = this;
    var url = $(this).val();
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#biciPhoto').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }  
});






const subStrInp = 'inp_'
const subStrView = 'priceView_'

function cangeTotalValues() {
    $('#totalValueDiv').load(`/Cart/GetTotalValue/`)
}

cangeTotalValues();

let views = document.getElementsByClassName('prView')
for (let view of views) {
    let id = view.id
    let biciId = id.substring(subStrView.length, id.length)
    $(`#${id}`).load(`/Cart/PriceView/?bicicletaId=${biciId}`)
}



function getBiciId(elem) {
    let strStartIndex = 3
    let btnId = elem.attr('id')
    return btnId.substring(strStartIndex, btnId.length)
}


$('.decrementBtn').click(function () {
    let biciId = getBiciId($(this))
    let inpId = subStrInp + biciId

    let currentVal = parseInt($(`#${inpId}`).val())
    let minVal = parseInt($(`#${inpId}`).attr('min'))
    if (currentVal != minVal) {
        currentVal -= 1;
        $(`#${inpId}`).val(currentVal)

        $(`#${subStrView}${biciId}`).load(`/Cart/PriceView/?bicicletaId=${biciId}&count=${currentVal}`, function () {
            cangeTotalValues()
        })
       
    }
})
$('.countInput').change(function () {
    let minVal = parseInt($(this).attr('min'))
    let maxVal = parseInt($(this).attr('max'))
    if ($(this).val() < minVal)
        $(this).val(minVal)
    if ($(this).val() > maxVal)
        $(this).val(maxVal)

    if (typeof parseInt($(this).val()) == 'number') {
        let inpId = $(this).attr('id')
        let biciId = inpId.substring(subStrInp.length, inpId.length)

        $(`#${subStrView}${biciId}`).load(`/Cart/PriceView/?bicicletaId=${biciId}&count=${$(this).val()}`, function () {
            cangeTotalValues()
        })
       
    }
})
$('.incrementBtn').click(function () {
    let biciId = getBiciId($(this))
    let inpId = subStrInp + biciId
    let currentVal = parseInt($(`#${inpId}`).val())
    let maxVal = parseInt($(`#${inpId}`).attr('max'))
    if (currentVal != maxVal) {
        currentVal += 1;
        $(`#${inpId}`).val(currentVal)
        $(`#${subStrView}${biciId}`).load(`/Cart/PriceView/?bicicletaId=${biciId}&count=${currentVal}`, function () {
            cangeTotalValues()
        })
       
    }
})