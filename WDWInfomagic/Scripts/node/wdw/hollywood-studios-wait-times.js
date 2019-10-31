// include the Themeparks library
const Themeparks = require("themeparks");

// configure where SQLite DB sits
// optional - will be created in node working directory if not configured
// Themeparks.Settings.Cache = __dirname + "/themeparks.db";

// access a specific park
//  Create this *ONCE* and re-use this object for the lifetime of your application
//  re-creating this every time you require access is very slow, and will fetch data repeatedly for no purpose
const park = new Themeparks.Parks.WaltDisneyWorldHollywoodStudios();

// Access wait times by Promise
const CheckWaitTimes = () => {
    park.GetWaitTimes().then((rideTimes) => {
        rideTimes.forEach((ride) => {
            console.log(JSON.stringify(ride))
            //console.log(`${ride.name}: ${ride.waitTime} minutes wait (${ride.status})`);
        });
    }).catch((error) => {
        console.error(error);
    }).then(() => {
        // setTimeout(CheckWaitTimes, 1000 * 60 * 5); // refresh every 5 minutes
        process.exit(1)
    });
};
CheckWaitTimes();

// you can also call GetOpeningTimes on themeparks objects to get park opening hours