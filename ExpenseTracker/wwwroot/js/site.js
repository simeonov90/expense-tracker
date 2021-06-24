const btnExpense = document.getElementById('btn-expense');
const btnIncome = document.getElementById('btn-income');
// get income and expense form
const incomeForm = document.getElementById('income-form');
const expenseForm = document.getElementById('expense-form');
// get income and expense div 
const incomeSubmitForm = document.getElementById('incomeSubmitForm');
const expenseSubmitForm = document.getElementById('expenseSubmitForm');
// get input value
const incomeFrom = document.getElementById('income-from');
const incomeValue = document.getElementById('income-value');
const expenseFrom = document.getElementById('expense-from');
const expenseValue = document.getElementById('expense-value');

const table = document.getElementById('table');
const dailyHistory = document.getElementById('daily-history');

let check = "";

incomeSubmitForm.style.display = "none";
expenseSubmitForm.style.display = "none";

async function getAllIncomes() {
    const response = await fetch('Income/GetAllIncomes');
    const responeData = await response.json();
    
    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            `<tr>
                 <td scope="row">Income</td>
                 <td>${item.incomeFrom}</td>
                 <td>${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

        table.innerHTML += div;
    });
}

async function getDailyIncomes() {
    const response = await fetch('Income/GetDailyIncomes');
    const responeData = await response.json();

    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            `<tr>
                 <td scope="row">Income</td>
                 <td>${item.incomeFrom}</td>
                 <td>${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

        table.innerHTML += div;
    });
}

function closeForm() {
    expenseSubmitForm.style.display = "none";
    incomeSubmitForm.style.display = "none";
    incomeForm.reset();
    expenseForm.reset();
}

function showIncomeSubmitForm() {
    check = "income";

    if (expenseSubmitForm.style.display === "block") {
        expenseSubmitForm.style.display = "none"

    }

    if (incomeSubmitForm.style.display === "none") {
        incomeSubmitForm.style.display = "block";
    } else {
        incomeSubmitForm.style.display = "none";
    }
}

function showExpenseSubmitForm() {
    check = "expense";
    if (incomeSubmitForm.style.display === 'block') {
        incomeSubmitForm.style.display = "none"
    }

    if (expenseSubmitForm.style.display === "none") {
        expenseSubmitForm.style.display = "block";
    } else {
        expenseSubmitForm.style.display = "none";
    }
}

async function sendRequest() {    
    if (check === "income") {
        let url = '/Income/CreateIncome';
        let xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                getDailyIncomes();
            }
        };
        xhr.open("POST", url, true)
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
        let body = await JSON.stringify({ incomeFrom: incomeFrom.value, value: incomeValue.value });
        xhr.send(body);

        incomeForm.reset();
    }

    if (check === "expense") {
        let url = '/Expense/CreateExpense';
        let xhr = new XMLHttpRequest();  
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                getDailyIncomes();
            }
        };
        xhr.open("POST", url, true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        let body = await JSON.stringify({ expenseFrom: expenseFrom.value, value: expenseValue.value });
        xhr.send(body);
        
        expenseForm.reset();
    }   
}

$(table).ready(function () {
    getDailyIncomes();
});






