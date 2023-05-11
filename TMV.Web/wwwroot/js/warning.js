const synth = window.speechSynthesis;
let useVoice = {};

function populateVoiceList() {
    let voices = synth.getVoices().sort(function (a, b) {
        const aname = a.name.toUpperCase();
        const bname = b.name.toUpperCase();

        if (aname < bname) {
            return -1;
        } else if (aname == bname) {
            return 0;
        } else {
            return +1;
        }
    });
    useVoice = voices.find(x => x.lang == "zh-CN");
}

populateVoiceList();

if (speechSynthesis.onvoiceschanged !== undefined) {
    speechSynthesis.onvoiceschanged = populateVoiceList;
}

window.speak = (words) => {
    console.log(words)
    if (synth.speaking) {
        console.error("speechSynthesis.speaking");
        return;
    }

    if (words !== "") {
        const utterThis = new SpeechSynthesisUtterance(words);

        utterThis.onend = function (event) {
            console.log("SpeechSynthesisUtterance.onend");
        };

        utterThis.onerror = function (event) {
            console.error("SpeechSynthesisUtterance.onerror");
        };
        utterThis.voice = useVoice;
        utterThis.pitch = 1;
        utterThis.rate = 1;
        synth.speak(utterThis);
    }
}
