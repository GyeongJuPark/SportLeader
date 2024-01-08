$(document).ready(function () {
    allLeaderList()
    setupDropdownMenu();
});

// orgLeaders <-- 서버에서 가져온 전체 데이터
// leaders <-- 필터링된 데이터
// currentPageLeaders <-- 현재 페이지에 해당되는 데이터

var orgLeaders = [];
var filteredLeaders = [];


// [ 지도자 상세정보 ] 근무이력 
function updateWorkHistory(workHistory) {
    var historyInfoTable = $("#historyInfoTableBody");
    historyInfoTable.empty();

    if (workHistory && workHistory.length > 0) {
        for (var i = 0; i < workHistory.length; i++) {
            var history = workHistory[i];
            historyInfoTable.append(
                "<tr>" +
                "<td>" + history.schoolName + "</td>" +
                "<td>" + history.startDT + "</td>" +
                "<td>" + history.endDT + "</td>" +
                "<td>" + history.sportName + "</td>" +
                "</tr>"
            );
        }
    } else {
        console.warn("근무 이력 데이터가 없습니다.");
    }
}

// [ 지도자 상세정보 ] 자격사항 
function updateCertificates(certificates) {
    var certificateInfoTable = $("#certificateInfoTableBody");
    certificateInfoTable.empty();

    if (certificates && certificates.length > 0) {
        for (var i = 0; i < certificates.length; i++) {
            var certificate = certificates[i];
            certificateInfoTable.append(
                "<tr>" +
                "<td>" + certificate.certificateName + "</td>" +
                "<td>" + certificate.certificateNumber + "</td>" +
                "<td>" + certificate.certificateDT + "</td>" +
                "<td>" + certificate.organization + "</td>" +
                "</tr>"
            );
        }
    } else {
        console.warn("자격사항 데이터가 없습니다.");
    }
}

// [ 지도자 관리 ] - 검색기능
function searchInput() {
    var searchInput = $("#searchInput").val() ?? '';
    var searchType = $(".btn-select").text();
    
    let filteredLeaders = searchInput !== ''
        ? filterLeaders(searchInput, searchType)
        : orgLeaders;

    totalPages = calculateTotalPages(filteredLeaders.length, itemsPerPage);
    drawLeaderTable(filteredLeaders, currentPage);
    updatePaginationButtons(totalPages);
}


function filterLeaders(searchInput, searchType) {
    return orgLeaders.filter(function (leader) {
        if (searchType === "전체") {
            return leader.leaderName.includes(searchInput)
                || leader.sportsNo.includes(searchInput)
                || leader.leaderNo.includes(searchInput)
                || leader.schoolNo.includes(searchInput);
        } else if (searchType === "이름") {
            return leader.leaderName.includes(searchInput);
        } else if (searchType === "종목") {
            return leader.sportsNo.includes(searchInput);
        }
        return false;
    });
}

function toggleAllCheckboxes() {
    $(".rowCheckbox").prop("checked", $("#selectAll").prop("checked"));
    btnToggleDelete();
}

function btnToggleDelete() {
    var selectedRowCount = $(".rowCheckbox:checked").length;
    var btnDeleteLeader = $(".btnDeleteLeader");

    if (selectedRowCount > 0) {
        btnDeleteLeader.addClass("active").prop("disabled", false);
    } else {
        btnDeleteLeader.removeClass("active").prop("disabled", true);
    }
}

// 커스텀 Select
function setupDropdownMenu() {
    const btn = document.querySelector('.btn-select');
    const list = document.querySelector('.list-member');

    btn.addEventListener('click', () => {
        btn.classList.toggle('on');
    });

    list.addEventListener('click', (event) => {
        if (event.target.nodeName === "BUTTON") {
            btn.innerText = event.target.innerText;
            btn.classList.remove('on');
        }
    });
}

// 지도자 삭제 기능
function deleteSelectedLeaders() {
    var selectedLeaderNos = [];

    document.querySelectorAll('.rowCheckbox:checked').forEach(checkbox => {
        selectedLeaderNos.push(checkbox.value);
    });
    console.log(selectedLeaderNos);

    if (selectedLeaderNos.length > 0) {
        $.ajax({
            url: '/Home/Delete',
            type: 'DELETE',
            contentType: 'application/json',
            data: JSON.stringify(selectedLeaderNos),
            success: function (data) {
                window.location.href = '/Home/Index';
                console.log("삭제 성공");
            },
            error: function (error) {
                console.error('삭제 실패', error);
            }
        });
    }
}
function MoveRegisterPage() { 
     window.location.href = '/Home/Register';
}


// 페이지네이션
function goToPrevPage() {
    if (currentPage > 1) {
        currentPage--;
        drawLeaderTable(filteredLeaders, currentPage);
    }
}

function goToPage(pageNumber) {
    currentPage = pageNumber;
    drawLeaderTable(filteredLeaders, pageNumber);
}

function goToNextPage() {
    if (currentPage < totalPages) {
        currentPage++;
        drawLeaderTable(filteredLeaders, currentPage);
    }
}

var currentPage = 1;
var itemsPerPage = 10;
var totalPages;

function calculateTotalPages(totalItems, itemsPerPage) {
    return Math.ceil(totalItems / itemsPerPage);
}


function allLeaderList() {
    $.ajax({
        type: "GET",
        url: "/api/alls",
        dataType: "json",

        success: function (data) {
            orgLeaders = data;
            filteredLeaders = data;
            totalPages = calculateTotalPages(data.length, itemsPerPage)
            drawLeaderTable(data, currentPage);
        },
        error: function () {
            alert('데이터를 불러오는데 실패했습니다.');
        }
    });
}

function drawLeaderTable(data, currentPage) {

    //console.log(`테이블 그리기: ${data.length}`);
    $("#leaderListTableBody").empty();
    var startIndex = (currentPage - 1) * itemsPerPage;
    var endIndex = startIndex + itemsPerPage;

    if (data.length > 0) {
        for (var i = startIndex; i < endIndex && i < data.length; i++) {
            var item = data[i];

            $("#leaderListTable").append(
                "<tr>" +
                "<td><input type='checkbox' value='" + item.leaderNo + "' class= 'rowCheckbox' onchange = 'btnToggleDelete()' ></td > " +
                "<td>" + (i + 1) + "</td>" +
                "<td>" + item.leaderNo + "</td>" +
                "<td>" + item.leaderName + "</td>" +
                "<td>" + item.sportsNo + "</td>" +
                "<td>" + item.schoolNo + "</td>" +
                "<td><input type='button' onclick='MoveDetailPage(\"" + item.leaderNo + "\")' value='상세보기' /></td>" +
                "</tr>"
            );

        }
    }
    updatePaginationButtons(totalPages);


    $("#countLeader").text("총 " + data.length + "명");
}

function updatePaginationButtons(totalPages) {
    $(".pagination button").remove();
    $(".pagination").append("<button onclick='goToPrevPage()'><img src='../images/leftArrow.png' alt='Previous'></button>");

    for (var i = 1; i <= totalPages; i++) {
        var button = $("<button onclick='goToPage(" + i + ")'>" + i + "</button>");
        if (i === currentPage) {
            button.addClass("active");
        }
        $(".pagination").append(button);
    }

    $(".pagination").append("<button onclick='goToNextPage()'><img src='../images/rightArrow.png' alt='Next'></button>");
}
