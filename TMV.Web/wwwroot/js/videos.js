
window.onPlay = (heng) => {
    socket = io('ws://localhost:5000');
    if (heng === 1) {
        var img11 = document.getElementById('img11'),
            img12 = document.getElementById('img12');
        socket.on('data11', function (data) {
            img11.src = 'data:image/jpeg;base64,' + data;
        });
        socket.on('data12', function (data) {
            img12.src = 'data:image/jpeg;base64,' + data;
        });
    } else if (heng === 2) {

        var img21 = document.getElementById('img21'),
            img22 = document.getElementById('img22');
        socket.on('data21', function (data) {
            img21.src = 'data:image/jpeg;base64,' + data;
        });
        socket.on('data22', function (data) {
            img22.src = 'data:image/jpeg;base64,' + data;
        });
    } else if (heng === 3) {
        img31 = document.getElementById('img31'),
            img32 = document.getElementById('img32');
        socket.on('data31', function (data) {
            img31.src = 'data:image/jpeg;base64,' + data;
        });
        socket.on('data32', function (data) {
            img32.src = 'data:image/jpeg;base64,' + data;
        });
    }
}