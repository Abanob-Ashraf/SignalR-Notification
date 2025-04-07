
$(function () {
    //let name = prompt("Enter your name") || "Guest";
    //let name = $("#UName").val().trim() || "Guest";


    console.log("Checking SignalR connection:", $.connection);

    if (!$.connection) {
        console.error("SignalR $.connection is undefined! Check if SignalR script is loaded.");
        return;
    }

    //  let chatProxy = $.connection.chat;
    let notificationProxy = $.connection.notificationHub;

    if (/*!chatProxy || */ !notificationProxy) {
        console.error("SignalR hubs are not available! Ensure the server is running.");
        return;
    }

    // Start SignalR connection
    $.connection.hub.start().done(function () {
        console.log("SignalR connected successfully");
    }).fail(function (err) {
        console.error("SignalR connection failed:", err);
    });

    // Chat message handler
    //chatProxy.client.newMessage = function (n, m) {
    //    $("ul").append(`<li> ${n}: ${m} </li>`);
    //};

    // Notification handler
    notificationProxy.client.newNotification = function (notificationModel) {
        showBrowserNotification(notificationModel);
    };

    // Send message function
    //window.sendMessage = function () {
    //    let message = $("#txt").val().trim() || "New Post";
    //    let name = $("#UName").val().trim() || "Guest";
    //    if (message) {
    //        chatProxy.server.sendMessage(name,message).fail(function (err) {
    //            console.error("Error sending message:", err);
    //        });
    //    }
    //};

    // Push notification function
    ////window.pushNotification = function () {
    ////    let message = $("#txt").val().trim() || "New Post";
    ////    let name = $("#UName").val().trim() || "Guest";
    ////    if (message) {
    ////        notificationProxy.server.pushNotification(message).fail(function (err) {
    ////            console.error("Error pushing notification:", err);
    ////        });
    ////    }
    ////};

    // Browser notification function
    function showBrowserNotification(notificationModel) {
        if (!("Notification" in window)) {
            alert("This browser does not support desktop notifications.");
            return;
        }
        if (Notification.permission === "granted") {
            new Notification("New Post", { body: notificationModel.Message, icon: notificationModel.ImageUrl/*icon: "Images/AAIBlogo.svg", image: notificationModel.ImageUrl*/ }).onclick = () => {
                window.open(notificationModel.OnclickUrl);
            };
        } else if (Notification.permission !== "denied") {
            Notification.requestPermission().then(permission => {
                if (permission === "granted") {
                    new Notification("New Post", { body: notificationModel.Message, icon: notificationModel.ImageUrl/*icon: "Images/AAIBlogo.svg", image: notificationModel.ImageUrl*/ }).onclick = () => {
                        window.open(notificationModel.OnclickUrl);
                    };;

                }
            });
        }
    }
});

//$(function () {
//    //let name = prompt("Enter your name") || "Guest";
//    //let name = $("#UName").val().trim() || "Guest";


//    console.log("Checking SignalR connection:", $.connection);

//    if (!$.connection) {
//        console.error("SignalR $.connection is undefined! Check if SignalR script is loaded.");
//        return;
//    }

//    //let chatProxy = $.connection.chat;
//    let notificationProxy = $.connection.notification;

//    if (/*!chatProxy || */ !notificationProxy) {
//        console.error("SignalR hubs are not available! Ensure the server is running.");
//        return;
//    }

//    // Start SignalR connection
//    $.connection.hub.start().done(function () {
//        console.log("SignalR connected successfully");
//    }).fail(function (err) {
//        console.error("SignalR connection failed:", err);
//    });

//    //// Chat message handler
//    //chatProxy.client.newMessage = function (m) {
//    //    console.log(`New message from ${m.Name}: ${m.Message1}`);
//    //    $("ul").append(`<li> ${m.Name}: ${m.Message1} </li>`);
//    //};

//    // Notification handler
//    notificationProxy.client.newNotification = function (title, message) {
//        console.log(`Received push notification: ${title} - ${message}`);
//        showBrowserNotification(title, message);
//    };

//    //// Send message function
//    //window.sendMessage = function () {
//    //    let message = $("#txt").val().trim() || "Ne w Post";
//    //    let name = $("#UName").val().trim() || "Guest";
//    //    if (message) {
//    //        chatProxy.server.sendMessage({ name: name, message1: message }).fail(function (err) {
//    //            console.error("Error sending message:", err);
//    //        });
//    //    }
//    //};

//    // Push notification function
//    window.pushNotification = function () {
//        let message = $("#txt").val().trim() || "New Post";
//        let name = $("#UName").val().trim() || "Guest";
//        if (message) {
//            notificationProxy.server.pushNotification(name, message).fail(function (err) {
//                console.error("Error pushing notification:", err);
//            });
//        }
//    };

//    // Browser notification function
//    function showBrowserNotification(title, message) {
//        if (!("Notification" in window)) {
//            alert("This browser does not support desktop notifications.");
//            return;
//        }

//        if (Notification.permission === "granted") {
//            new Notification(title, { body: message });
//        } else if (Notification.permission !== "denied") {
//            Notification.requestPermission().then(permission => {
//                if (permission === "granted") {
//                    new Notification(title, { body: message });
//                }
//            });
//        }
//    }
//});








////$(function () {
////    name = prompt("enter your name")
////    prox = $.connection.chat;

////    $.connection.hub.start();

////    prox.client.newMessage = function (n, m) {
////        $("ul").append(`<li> ${n} : ${m} </li>`)
////    }
////})
////function send() {
////    prox.server.sendMessage(name, $("#txt").val());
////}











//$(function () {
//    name = prompt("enter your name")
//    prox = $.connection.chat;

//    $.connection.hub.start();

//    prox.client.newMessage = function (n, m) {
//        $("ul").append(`<li> ${n} : ${m} </li>`)
//    }
//})
//function send() {
//    prox.server.sendMessage(name, $("#txt").val());
//}