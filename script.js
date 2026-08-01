
const my_name = prompt("enter your name")

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7010/chat")
    .withAutomaticReconnect()
    .build();

    

connection.start();
//alert("connection started")
connection.on("newmessage",(n,m)=>{
    const li = document.createElement("li");
    li.textContent = `${n}: ${m}`;
    document.getElementById("ul").appendChild(li);
    //alert("message brought from server broadcast")
})


 function send(event)
{
    
    connection.invoke("sendMessage",my_name,document.getElementById("text").value)
}