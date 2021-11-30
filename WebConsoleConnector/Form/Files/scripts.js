const HIDE = 0;
const SHOW = 1;
const ENABLE = 2;
const DISABLE = 3;



function init() {
    window.setInterval(getActions, 2000);
    document.body.style.backgroundColor = '#cccccc';
    }

function update(id, value) {
    let element = document.getElementById(id);
    switch (element.tagName) {
        case 'DIV':
            element.innerHTML = value;
            break;
        case 'INPUT':
            element.value = value;
            break;
        case 'BUTTON':
            element.innerHTML = value;
            break;
        }
    }

function change(id, change) {
    let element = document.getElementById(id);
    switch (change) {
        case HIDE: element.hidden = true; break;
        case SHOW: element.hidden = false; break;
        case ENABLE:
            if (element.tagName == 'BUTTON' || element.tagName == 'INPUT')
                element.disabled = false;
            break;
        case DISABLE:
            if (element.tagName == 'BUTTON' || element.tagName == 'INPUT')
                element.disabled = true;
            break;
        }
    }

function insert(id, html) {
    let parent = document.getElementById(id);
    parent.insertAdjacentHTML('beforeend', html);
    }

function handleActions() {
    if (this.readyState != XMLHttpRequest.DONE || this.status != 200) return;
    let actions = JSON.parse(this.responseText);
    for (index in actions) {
        let action = actions[index];
        switch (action.$type) {
            case 'UpdateAction':
                update(action.Id, action.Value);
                break;
            case 'InsertAction':
                insert(action.Id, action.Html);
                break;
            case 'ChangeAction':
                change(action.Id, action.Change);
                break;
            }
        }
    }

function getActions() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = handleActions;
    xhttp.open('GET', 'actions');
    xhttp.send();
    }

function sendAction(action) {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', '/action');
    xhttp.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    xhttp.onreadystatechange = handleActions;
    xhttp.send(JSON.stringify(action));
    }

function sendClickAction(element) {
    sendAction({ $type: 'ClickAction', Id: element.id });
    }

function sendUpdateAction(element) {
    sendAction({ $type: 'UpdateAction', Id: element.id, Value: element.value });
    }
