// include the Themeparks library
const Themeparks = require("themeparks");
const fs = require("fs");

// configure where SQLite DB sits
// optional - will be created in node working directory if not configured
// Themeparks.Settings.Cache = __dirname + "/themeparks.db";

// access a specific park
//  Create this *ONCE* and re-use this object for the lifetime of your application
//  re-creating this every time you require access is very slow, and will fetch data repeatedly for no purpose
const mk = new Themeparks.Parks.WaltDisneyWorldMagicKingdom();
const ak = new Themeparks.Parks.WaltDisneyWorldAnimalKingdom();
const ep = new Themeparks.Parks.WaltDisneyWorldEpcot();
const hs = new Themeparks.Parks.WaltDisneyWorldHollywoodStudios();
const refreshInverval = 1

GetStamp = (park) => {
    var d = new Date()
    var y = d.getFullYear()
    var m = d.getMonth()+1
    var da = d.getDate()
    var hr = d.getHours()
    var mn = d.getMinutes()
    var s = d.getSeconds()
    hr = hr < 10 ? `0${hr}` : hr
    mn = mn < 10 ? `0${mn}` : mn
    s = s < 10 ? `0${s}` : s
    var stamp = `${y}-${m}-${da}_${hr}:${mn}:${s}`
    console.log(`[${stamp}] ${park}`)
}
WriteData = (park, data) => {
    let f = `json\\${park}.json`
    if (fs.existsSync(f)) {
        fs.unlinkSync(f, (unlinkErr) => {
            if (unlinkErr) {
                console.log('ERROR DELETING FILE')
                console.log(unlinkErr)
            }
            console.log("File deleted.")
        })
    }
    fs.writeFile(f, data, (err) => {
        if (err) {
            console.log("ERROR WRITING TO FILE")
            console.log(err)
        }
        console.log(`${f} wait times saved to file.`)
    })
}

// Access wait times by Promise
const CheckMKWaitTimes = () => {
    GetStamp('MAGIC_KINGDOM_START')
    mk.GetWaitTimes().then((rideTimes) => {
        WriteData('mk', JSON.stringify(rideTimes))
    }).catch((error) => {
        console.error(error);
    }).then(() => {
        GetStamp('MAGIC_KINGDOM_END')
        setTimeout(CheckMKWaitTimes, 1000 * 60 * refreshInverval); // refresh every 5 minutes
    });
};
const CheckAKWaitTimes = () => {
    GetStamp('ANIMAL_KINGDOM_START')
    ak.GetWaitTimes().then((rideTimes) => {
        WriteData('ak', JSON.stringify(rideTimes))
    }).catch((error) => {
        console.error(error);
    }).then(() => {
        GetStamp('ANIMAL_KINGDOM_END')
        setTimeout(CheckAKWaitTimes, 1000 * 60 * refreshInverval); // refresh every 5 minutes
    });
};
const CheckHSWaitTimes = () => {
    GetStamp('HOLLYWOOD_STUDIOS_START')
    hs.GetWaitTimes().then((rideTimes) => {
        WriteData('hs', JSON.stringify(rideTimes))
    }).catch((error) => {
        console.error(error);
    }).then(() => {
        GetStamp('HOLLYWOOD_STUDIOS_END')
        setTimeout(CheckHSWaitTimes, 1000 * 60 * refreshInverval); // refresh every 5 minutes
    });
};
const CheckEPWaitTimes = () => {
    GetStamp('EPCOT_START')
    ep.GetWaitTimes().then((rideTimes) => {
        WriteData('ep', JSON.stringify(rideTimes))
    }).catch((error) => {
        console.error(error);
    }).then(() => {
        GetStamp('EPCOT_END')
        setTimeout(CheckEPWaitTimes, 1000 * 60 * refreshInverval); // refresh every 5 minutes
    });
};
CheckMKWaitTimes();
CheckAKWaitTimes();
CheckHSWaitTimes();
CheckEPWaitTimes();

// you can also call GetOpeningTimes on themeparks objects to get park opening hours