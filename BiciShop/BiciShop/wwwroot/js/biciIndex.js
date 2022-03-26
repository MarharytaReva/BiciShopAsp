const deleteBtnClassName = 'deleteForm'
const editBtnClassName = 'editForm'
const rowIdPart = 'r'
let propNames = ['title', 'price', 'weight', 'color', 'issueYear', 'wheelDiameter']

let token = 'token'

function activateBlur(state) {
    let blur = document.getElementById('blur')
    blur.style.display = state
}
function activeInfo(state) {
    let infoForm = document.getElementById('infoForm')
    infoForm.style.display = state
    activateBlur(state)
}
function activateBlurErrors(state) {
    let blur = document.getElementById('blurError')
    blur.style.display = state
}
function activeErrorsWindow(state) {
    let infoForm = document.getElementById('alertWindow')
    infoForm.style.display = state
    activateBlur(state)
}
function activateLoader(state) {
    activateBlur(state)
    let loader = document.getElementById('load')
    loader.style.display = state
}
$('#OkErrors').click(function () {
    activateBlurErrors('none')
    activeErrorsWindow('none')
    $('#errorsList').html('')
})

function checkAccess(response) {
    if (response.status === 403) {
        window.location.href = 'loginPage.html'
    }
}

function setListeners() {
    $(`.${deleteBtnClassName}`).submit(function (e) {

        e.preventDefault()
        let idInp = $(this).find('input[name=bicicletaId]')
        let id = idInp.val()

        deleteMethod(id)
    })
    $(`.${editBtnClassName}`).on('submit', function (e) {
        let binded = editEvent.bind(this)
        binded(e)
    })
}

function getForm(id, subValue, subColor, formClassName) {

    let td = document.createElement('td')
    let formElem = document.createElement('form')
    formElem.setAttribute('class', `${formClassName}`)

    let hiddenInp = document.createElement('input')
    hiddenInp.setAttribute('type', 'hidden')
    hiddenInp.setAttribute('value', `${id}`)
    hiddenInp.setAttribute('name', 'bicicletaId')

    let sub = document.createElement('input')
    sub.setAttribute('type', 'submit')
    sub.setAttribute('value', `${subValue}`)
    sub.setAttribute('class', `btn btn-${subColor}`)

    formElem.appendChild(hiddenInp)
    formElem.appendChild(sub)
    td.appendChild(formElem)

   
    return td
}

function getImageTd(photoStr) {
   
    let td = document.createElement('td')
    let img = document.createElement('img')
    img.src = `data:image/png;base64,${photoStr}`
    img.classList.add('biciPhoto')
    td.appendChild(img)
    
    return td
}

function getTableRow(bici) {
    let tr = document.createElement('tr')
    tr.setAttribute('id', `${rowIdPart}${bici.bicicletaId}`)
    tr.appendChild(getImageTd(bici.photo))

    for (let name of propNames) {
        let td = document.createElement('td')     
        td.append(bici[name])
        tr.appendChild(td);
    }
    let formDelete = getForm(bici.bicicletaId, 'Delete', 'danger', deleteBtnClassName)
    let formEdit = getForm(bici.bicicletaId, 'Edit', 'warning', editBtnClassName)

    tr.appendChild(formDelete)
    tr.appendChild(formEdit)

    return tr
}

async function getByIdMethod(id) {
    let accessToken = sessionStorage.getItem(token)
    activateBlurErrors('block')
    activateLoader('block')
    let response = await fetch(`/bicis/${id}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
       
    })
    if (response) {
        let bici = await response.json()
        activateBlurErrors('none')
        activateLoader('none')
        return bici
    }
    activateBlurErrors('none')
    activateLoader('none')
}

async function getMethod() {
    let accessToken = sessionStorage.getItem(token)
    activateBlurErrors('block')
    activateLoader('block')
    let response = await fetch('/bicis', {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    })
    
    if (response.ok === true) {
        let arr = await response.json()
       
        let tableBody = document.querySelector('tbody')
        for (let bici of arr) {
            tableBody.appendChild(getTableRow(bici))
        }
        setListeners()
    }
    activateBlurErrors('none')
    activateLoader('none')
}

function Bicicleta(title, price, weight, issueYear, color, wheelDiameter, biciTypeId, bicicletaId, photo) {
    this.title = title
    this.price = !price ? -1 : price
    this.weight = !weight ? -1 : weight
    this.issueYear = !issueYear ? -1 : issueYear
    this.color = color
    this.wheelDiameter = !wheelDiameter ? -1 : wheelDiameter
    this.biciTypeId = biciTypeId
    this.bicicletaId = bicicletaId
    this.photo = photo
}

function showErrors(errors) {
   
    for (let name of Object.getOwnPropertyNames(errors)) {
        $(`#${name}`).text(errors[name]).css('display', 'block')
    }
   
}

async function postMethod(bici) {
    let accessToken = sessionStorage.getItem(token)
    activateBlurErrors('block')
    activateLoader('block')
    let response = await fetch('/bicis', {
        method: 'POST',
        headers: {
            'Accept': 'application/json', 'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        },
        body: JSON.stringify(bici)
    })
    checkAccess(response)
    if (response.ok === true) {
        let createdBici = await response.json()
        let tbody = document.querySelector('tbody')
        tbody.appendChild(getTableRow(createdBici))
        
        clearCreateForm()
    } else {
        let errors = await response.json()
        showErrors(errors['errors'])
        console.log(errors)
    }
   
    activateLoader('none')
}
async function deleteMethod(id) {
    let accessToken = sessionStorage.getItem(token)
    activateBlurErrors('block')
    activateLoader('block')
    let response = await fetch(`/bicis/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'bearer ' + accessToken
        }
    })
    checkAccess(response)
    if (response.ok === true) {
        let deletedBici = await response.json()
        let tbody = document.querySelector('tbody')
        let rowForDelete = document.getElementById(`${rowIdPart}${deletedBici.bicicletaId}`);
        tbody.removeChild(rowForDelete);
    }
    activateBlurErrors('none')
    activateLoader('none')
}
async function putMethod(bici) {
    let accessToken = sessionStorage.getItem(token)
    activateBlurErrors('block')
    activateLoader('block')
    let response = await fetch('/bicis', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json', 'Content-Type': 'application/json',
            'Authorization': 'bearer ' + accessToken
        },
        body: JSON.stringify(bici)
    })
    
    checkAccess(response)
    if (response.ok === true) {
        let updatedBici = await response.json()
        let tbody = document.querySelector('tbody')
        let rowForDelete = document.getElementById(`${rowIdPart}${updatedBici.bicicletaId}`);
        tbody.removeChild(rowForDelete);
        console.log('put ok')
        tbody.appendChild(getTableRow(updatedBici));
        clearCreateForm()
    } else {
        let errors = await response.json()
        showErrors(errors['errors'])
        console.log(errors)
    }
   
    activateLoader('none')
}

function clearCreateForm() {
    let form = document.forms['createForm']
    for (let name of Object.getOwnPropertyNames(form.elements)) {
        form.elements[name].value = ''
    }
    $('#subCreate').val('Save')

    $('.alert').css('display', 'none')
    activateBlurErrors('none')
    activateBlur('none')
    activeInfo('none')
}

$('#showCreateFormBtn').click(function () {
    activateBlur('block')
    activeInfo('block')
})
$('#close').click(function () {
    clearCreateForm();
})


function subCreateFunc(img) {
    let form = document.forms['createForm']

    let id = form.elements['id'].value
    console.log(id)

    let photoStr = img
    console.log('pered if', photoStr)
    if (photoStr.length === 0) {
        console.log('if srab')
        let photoFromHidden = form.elements['photo'].value
        photoStr = !photoFromHidden ? defaultImageStr : photoFromHidden
    }
    console.log(photoStr)

    let title = form.elements['title'].value
    let price = parseFloat(form.elements['price'].value)
    let weight = parseFloat(form.elements['weight'].value)
    let issueYear = parseInt(form.elements['issueYear'].value)
    let color = form.elements['color'].value
    let wheelDiameter = parseFloat(form.elements['wheelDiameter'].value)
    if (!id) {
        let bici = new Bicicleta(title, price, weight, issueYear, color, wheelDiameter, 1, 0, photoStr)
        postMethod(bici);
    } else {
        let bici = new Bicicleta(title, price, weight, issueYear, color, wheelDiameter, 1, parseInt(id), photoStr)
        putMethod(bici)
    }


    setListeners()
}
function imageUpload() {
    try {
        let base64String = ""
        var file = document.querySelector('input[type=file]')['files'][0];

        var reader = new FileReader();

        reader.onload = function () {
            base64String = reader.result
            let index = base64String.indexOf(',')
            base64String = base64String.slice(index + 1)

            subCreateFunc(base64String)
        }

        reader.readAsDataURL(file);
       
    } catch (e) {
        subCreateFunc('')
    }
}
let defaultImageStr = "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";

$('#subCreate').click(function (e) {
    e.preventDefault()
    imageUpload()
})

async function editEvent(e) {
    e.preventDefault()
    let id = $(this).find('input[name=bicicletaId]').val()
    console.log($(this))
    console.log(id)
    let bici = await getByIdMethod(id)
   
    console.log(bici)
    let form = document.forms['createForm']
    form.elements['photo'].value = bici.photo
    form.elements['id'].value = bici.bicicletaId
    form.elements['title'].value = bici.title
    form.elements['price'].value = bici.price
    form.elements['weight'].value = bici.weight
    form.elements['issueYear'].value = bici.issueYear
    form.elements['wheelDiameter'].value = bici.wheelDiameter
    form.elements['color'].value = bici.color

    activateBlur('block')
    activeInfo('block') 
}



if (!window.location.href.includes('loginPage.html')) {
    console.log(window.location.href)
    if (sessionStorage.getItem(token)) {
        getMethod()
    } else {
        window.location.href = 'loginPage.html'
    }
}






function showErrorAlert(errors) {
    $('#errors').text(errors).css('display', 'block')

}
async function getToken() {
    let credentials = {
        login: document.forms['LoginForm'].elements['login'].value,
        password: document.forms['LoginForm'].elements['password'].value
    }
    console.log('login', credentials.login)
    console.log('password', credentials.password)
    let response = await fetch('/api/account/token', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
    })

    let responseData = await response.json()
    if (response.ok === true) {
        sessionStorage.setItem(token, responseData.access_token)
        window.location.href = 'biciIndex.html'
    } else {
        showErrorAlert(responseData.errors)
    }
}

$('#loginBtn').click(function (e) {
    e.preventDefault()
    getToken()
})