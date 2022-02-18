
document.querySelector('.controls')?.addEventListener('submit', event => 
{
    event.preventDefault();
});

$("input").keypress(function(event) 
{
    if (event.which == 13)
    {
        search();
    }
});

function search()
{
    $.ajax(
    {
        type: "POST",
        url: "Person/Search",
        data: {'searchString' : $("#searchString").val()},
        success: function(data, status)
        {
            console.log("Data: " + data + "\nStatus: " + status);
            $(".container").html(data);
        }
    });
}

function listPeople()
{
    $.ajax(
    {
        type: "GET",
        url: "Person/ListPeople",
        success: function(data, status)
        {
            console.log("Data: " + data + "\nStatus: " + status);
            $(".container").html(data);
        }
    });
}

function showPerson()
{
    var id = $("#personId").val();
    $.ajax(
    {
        type: "GET",
        url: "Person/ShowPerson",
        data: {
            personId: id
        },
        success: function(data, status)
        {
            console.log("Data: " + data + "\nStatus: " + status);
            $(".container").html('<div class="alert alert-success">' + data + '</div>');
        }
    });
}

function deletePerson()
{
    var id = $("#personId").val();
    $.ajax(
    {
        type: "GET",
        url: "Person/DeletePerson",
        data: {
            personId: id
        },
        success: function(data, status)
        {
            console.log("Data: " + data + "\nStatus: " + status);
            $(".container").html('<div class="alert alert-success">' + data + '</div>');
        }
    });
}
