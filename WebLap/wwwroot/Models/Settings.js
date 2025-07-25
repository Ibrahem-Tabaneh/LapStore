
var ClsSettings = {
    GetAll: function () {
        Helper.AjaxCallGet("/api/Settings", {}, "json",
            function (data) {
                let phones = document.querySelectorAll(".PhoneNumber");
                let namesstore = document.querySelectorAll(".NameStore");
                let descstore = document.getElementById("DescStore2");
                let EmailUs = document.getElementById("EmailUs");
                let AddressDeveloper = document.getElementById("AddressDeveloper");
                let twitter = document.getElementById("twitter");
                let instgram = document.getElementById("instgram");
                let LogosWebsite = document.querySelectorAll(".LogosWebsite");

                

                


                instgram.href = data.data.instgramLink;
                twitter.href = data.data.twitterLink;
                descstore.innerHTML = data.data.websiteDescription;
                EmailUs.innerHTML = data.data.email;
                AddressDeveloper.innerHTML = data.data.address;

                LogosWebsite.forEach(logo => {
                    logo.src = "/Uploads/LogoImg/"+data.data.logo;
                });

                phones.forEach(phone => {
                    phone.innerHTML = data.data.contactNumber;
                });

                namesstore.forEach(name => {
                    name.innerHTML = data.data.websiteName;
                });

                console.log(data);
            }, function () { } )
    }
}
ClsSettings.GetAll();
