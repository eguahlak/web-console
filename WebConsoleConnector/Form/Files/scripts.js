function init() {
    window.setInterval(getActions, 2000);
    document.body.style.backgroundColor = ""#ffeeee"";
}

function update(id, value) {
    let element = document.getElementById(id);
    switch (element.tagName) {
        case 'SPAN':
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

function insert(id, html) {
    let parent = document.getElementById(id);
    parent.insertAdjacentHTML('beforeend', html);

    //let template = document.createElement('template');
    //template.innerHtml = html.trim();
    //parent.appendChild(template.content.firstChild);
}



function getActions() {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            // alert(this.responseText);
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
                }
            }
        }
    };
    xhttp.open('GET', 'actions');
    xhttp.send();
}

function sendAction(action) {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', '/action');
    xhttp.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
    xhttp.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            // Nothing to do yet (fire and forget)
        }
    }
    xhttp.send(JSON.stringify(action));

    getActions();
}

function sendClickAction(element) {
    sendAction({ $type: 'ClickAction', Id: element.id });
}

function sendUpdateAction(element) {
    sendAction({ $type: 'UpdateAction', Id: element.id, Value: element.value });
}
