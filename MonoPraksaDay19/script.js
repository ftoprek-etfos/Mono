function showCrewmates()
{
    const crewmateList = JSON.parse(window.localStorage.getItem('crewmateList'));
    let table = `<tr>
                    <th>Name</th>
                    <th>Age</th>
                    <th>Role</th>
                    <th>Actions</th>
                </tr>`;
    if (crewmateList)
    {
        crewmateList.map(crewmate => {
            table += `<tr>
                        <td>${crewmate.name}</td>
                        <td>${crewmate.age}</td>
                        <td>${crewmate.role}</td>
                        <td>
                            <a class="Button" onclick="editCrewmate(${crewmate.id})">Edit</a>
                            <a class="Button" onclick="deleteCrewmate(${crewmate.id})">Delete</a>
                        </td>
                    </tr>`;
        });
    }else
    {
        table = `<tr>
                    <td>No crewmates found!</td>
                </tr>`;
    }
    window.document.getElementById('crewmateList').innerHTML = table;
    window.document.getElementById('showCrewmatesBtn').style.display = 'none';
}

function addCrewmates()
{
    let crewmateList = [];
    if(window.localStorage.getItem('crewmateList'))
    {
        crewmateList = JSON.parse(window.localStorage.getItem('crewmateList'));
    }
    if(window.document.forms.addCrewmateForm.name.value === '' || window.document.forms.addCrewmateForm.age.value === '' || window.document.forms.addCrewmateForm.role.value === '')
    {
        alert('Please fill all the fields!');
        return;
    }
    const crewmate = {
        id: crewmateList.length + 1,
        name: window.document.forms.addCrewmateForm.name.value,
        age: window.document.forms.addCrewmateForm.age.value,
        role: window.document.forms.addCrewmateForm.role.value
    }

    crewmateList.push(crewmate);
    window.localStorage.setItem('crewmateList', JSON.stringify(crewmateList));
    goBack();
    window.document.getElementById("statusText").innerText = "Crewmate added successfully!";
    setTimeout(function() {
        window.document.getElementById("statusText").display = "none";
        window.document.getElementById("statusText").innerText = "";
    }, 2000);
}

function deleteCrewmate(id)
{
    const response = confirm(`Are you sure you want to delete?`);
    if(!response)return;
    const crewmateList = JSON.parse(window.localStorage.getItem('crewmateList'));
    const updatedCrewmateList = crewmateList.filter(crewmate => crewmate.id !== id);
    if(updatedCrewmateList.length == 0)
    {
        window.localStorage.removeItem('crewmateList');
        alert(`Crewmate deleted successfully!`);
        showCrewmates();
        return;
    }
    window.localStorage.setItem('crewmateList', JSON.stringify(updatedCrewmateList));
    showCrewmates();
    window.document.getElementById("statusText").innerText = "Crewmate deleted successfully!";
    setTimeout(function() {
        window.document.getElementById("statusText").display = "none";
        window.document.getElementById("statusText").innerText = "";
    }, 2000);
}

function editCrewmate(id)
{
    document.title = "Edit Crewmate";
    window.document.getElementById('crewmateList').style.display = 'none';
    window.document.getElementById('addBtn').style.display = 'none';
    const crewmateList = JSON.parse(window.localStorage.getItem('crewmateList'));
    const crewmateToEdit = crewmateList.find(crewmate => crewmate.id === id);
    editCrewmateModal(crewmateToEdit);
}

function editCrewmateModal(crewmateToEdit)
{
    const html = `<div id="editCrewmateDiv">
    <h2>Edit crewmate</h2>
    <form id="editCrewmateForm" onSubmit="return false">
        <label for="name">Name:</label>
        <input type="text" id="editName" name="name" value='${crewmateToEdit.name}' required>
        <label for="age">Age:</label>
        <input type="number" id="editAge" name="age" value='${crewmateToEdit.age}' required>
        <label for="role">Role:</label>
        <input type="text" id="editRole" name="role" value='${crewmateToEdit.role}' required>
        <a class="Button" type="submit" id="editCrewmateBtn" onClick="applyEdit(${crewmateToEdit.id})"><h2>Edit crewmate</h2></a>
        <br/>
    </form>
</div>`;
    window.document.getElementById("mainDiv").innerHTML += html;
}

function applyEdit(id)
{
    let crewmateList = JSON.parse(window.localStorage.getItem('crewmateList'));
    let crewmateToEdit = crewmateList.find(crewmate => crewmate.id === id);
    crewmateList = crewmateList.filter(crewmate => crewmate.id !== id);
    crewmateToEdit.name = window.document.forms.editCrewmateForm.name.value;
    crewmateToEdit.age = window.document.forms.editCrewmateForm.age.value;
    crewmateToEdit.role = window.document.forms.editCrewmateForm.role.value;
    crewmateList.push(crewmateToEdit)
    window.localStorage.setItem('crewmateList', JSON.stringify(crewmateList));
    window.document.getElementById('editCrewmateDiv').remove();
    restoreToDefault();
    showCrewmates();
    window.document.getElementById("statusText").innerText = "Crewmate edited successfully!";
    setTimeout(function() {
        window.document.getElementById("statusText").display = "none";
        window.document.getElementById("statusText").innerText = "";
    }, 2000);
}

function openAddCrewmateModal()
{
    document.title = "Add Crewmate";
    window.document.getElementById('showCrewmatesBtn').style.display = 'none';
    window.document.getElementById('crewmateList').style.display = 'none';
    window.document.getElementById('addBtn').style.display = 'none';
    addCrewmateModal();
}

function goBack()
{
    window.document.getElementById('addCrewmateDiv').remove();
    restoreToDefault();
}

function restoreToDefault()
{
    document.title = "Crewmate logbook";
    if(window.document.getElementById("crewmateList").innerHTML.trim() == "")
    {
        window.document.getElementById('showCrewmatesBtn').style.display = 'block';
    }else
    {
        showCrewmates();
    }
    window.document.getElementById('crewmateList').style.display = 'block';
    window.document.getElementById('addBtn').style.display = 'block';
}

function addCrewmateModal()
{
    const html = `<div id="addCrewmateDiv">
    <h2>Add a new crewmate</h2>
    <form id="addCrewmateForm" onSubmit="return false">
        <label for="name">Name:</label>
        <input type="text" id="name" name="name" required>
        <label for="age">Age:</label>
        <input type="number" id="age" name="age" required>
        <label for="role">Role:</label>
        <input type="text" id="role" name="role" required>
        <a class="Button" id="showCrewmateList" onClick="addCrewmates()"><h2>Add crewmate</h2></a>
        <a class="Button" id="showCrewmateList" onClick="goBack()"><h2>Back</h2></a>
    </form>
    <br/>
    </div>`;
    window.document.getElementById("mainDiv").innerHTML += html;
}