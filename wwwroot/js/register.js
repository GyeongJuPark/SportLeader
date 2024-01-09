$(document).ready(function () {
    loadModalTable('/api/leaders', '#LeaderTableBody', 'LeaderNoModal');
    loadModalTable('/api/schools', '#SchoolTableBody', 'SchoolNameModal');
    loadSports();
});

// 이미지 크기 검사
let base64ImageData = null;
let Imagevalue = $('#images');
let LeaderImage = $('#LeaderImage');
function imageSizeCheck() {
    const fileInput = document.getElementById("input-image");
    const selectedImage = document.getElementById("img");
    const maxSizeInBytes = 4 * 1024 * 1024;

    const selectedFiles = fileInput.files;
    if (selectedFiles.length > 0) {
        const [imageFile] = selectedFiles;
        
        if (imageFile.size > maxSizeInBytes) {
            alert("이미지 크기는 4MB 이하이어야 합니다.");
            fileInput.value = "";
            return;
        }
        const fileReader = new FileReader();
    
        fileReader.onload = () => {
            const srcData = fileReader.result;
            selectedImage.src = srcData;
    
            base64ImageData = srcData.split(",")[1];
            Imagevalue.val(base64ImageData);
            LeaderImage.val(base64ImageData)
        };
        fileReader.readAsDataURL(imageFile);
    }
}
function cancel_addLeader() {
    var modal = $('#shortModal');
    modal.find('.modal-body h2').text('지도자 등록 취소');
    modal.find('.modal-body p').html('지도자 등록을 취소하시겠습니까?<br>작성한 내용은 모두 삭제됩니다.');

    var buttons = '<input class="btn btn-secondary" type="button" value="취소" onclick="closeShortModal()" style="margin-right: 8px;">';
    buttons += '<a href="/Home/Index"><input class="btn btn-primary" type="button" value="확인"></a>';
    modal.find('.modal-body .modal-buttons').html(buttons);

    modal.modal('show');
}

// 지도자 식별코드 검색
function searchLeaderList() {
    var searchModalInput = $('#LeaderModalInput').val();
    var tableRows = $('#LeaderTableBody').find('tr');

    tableRows.each(function () {
        var cells = $(this).find('td');
        var name = cells.eq(1).text();

        if (name.includes(searchModalInput)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

// 학교명(학교코드) 검색
function searchSchoolList() {
    var searchModalInput = $('#SchoolModalInput').val();
    var tableRows = $('#SchoolTableBody').find('tr');

    tableRows.each(function () {
        var cells = $(this).find('td');
        var name = cells.eq(1).text();

        if (name.includes(searchModalInput)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function selectRow(row) {
    var selectedRows = $('.selected-row');
    var clickedRow = $(row);

    if (clickedRow.hasClass('selected-row')) {
        clickedRow.removeClass('selected-row');
    } else {
        selectedRows.removeClass('selected-row');
        clickedRow.addClass('selected-row');
    }
}

function updateTable(data, searchType) {
    var tableBody = $('#' + (searchType === 'leaderList' ? 'leaderTableBody' : 'schoolTableBody'));

    tableBody.empty();

    data.forEach(function (row, index) {
        var newRow = '<tr onclick="selectRow(this)"><td>' + (index + 1) + '</td>';

        if (searchType === 'leaderList') {
            newRow += '<td>' + row.leaderNO + '</td>';
            newRow += '<td>' + row.leaderName + '</td>';
        } else if (searchType === 'schoolList') {
            newRow += '<td>' + row.schoolName + '</td>';
            newRow += '<td style="display:none">' + row.schoolNo + '</td>';
        }

        newRow += '</tr>';
        tableBody.append(newRow);
    });
}
function openShortModal(title, message) {
    var modal = $('#shortModal');
    modal.find('.modal-body h2').text(title);
    modal.find('.modal-body p').html(message);
    modal.modal('show');
}

function openLeaderModal() {
    var modal = $('#LeaderNoModal');
    modal.modal('show');
}

function openSchoolModal() {
    var modal = $('#SchoolNameModal');
    modal.modal('show');
}


function closeShortModal() {
    $('#shortModal').modal('hide');
}


function Item_Remove(obj) {
    $(obj).closest('tr').remove();
}

function cancelSchoolNameModal() {
    var modal = $('#SchoolNameModal');
    modal.modal('hide');
}function cancelLeaderNoModal() {
    var modal = $('#LeaderNoModal');
    modal.modal('hide');
}

function noSelectedLeader() {
    var selectedRows = $('.selected-row');
    var leaderCode = $('#LeaderNo');
    var leaderName = $('#LeaderName');
    var historyLeaderNo = $('#HistoryLeaderNo');
    var certificateLeaderNo = $('#CertificateLeaderNo');
    if (selectedRows.length > 0) {
        var cells = selectedRows.find('td');
        leaderCode.val(cells.eq(0).text());
        leaderName.val(cells.eq(1).text());
        historyLeaderNo.val(cells.eq(0).text());
        certificateLeaderNo.val(cells.eq(0).text());
        cancelLeaderNoModal();
    } else {
        openShortModal('선택된 식별코드 없음', '선택된 지도자 식별코드가 없습니다.<br>지도자 식별코드를 선택해주시기 바랍니다.');
    }
}

function noSelectedSchool() {
    var selectedRows = $('.selected-row');
    var schoolName = $('#SchoolName');
    var schoolCode = $('#SchoolNo');
    var historySchoolName = $('#historySchoolName');


    if (selectedRows.length > 0) {
        var cells = selectedRows.find('td');
        schoolName.val(cells.eq(1).text());
        historySchoolName.val(cells.eq(1).text());
        schoolCode.val(cells.eq(0).text());
        cancelSchoolNameModal();
    } else {
        openShortModal('선택된 학교 없음', '선택된 학교명이 없습니다.<br>학교명을 선택해주시기 바랍니다.');
    }
}

// 근무 이력 테이블 추가
var historyIndex = 1;
var certificateIndex = 1;
function addWorking() {
    loadSports();
    var addRows = $("#history_table tbody tr");
    for (var i = 0; i < addRows.length; i++) {
        var HistoryRow = addRows.eq(i);
        var HistorySchoolName = HistoryRow.find('input[name^="Histories["][name$="].SchoolName"]').val();
        var HistoryStartDT = HistoryRow.find('input[name^="Histories["][name$="].StartDT"]').val();
        var HistoryEndDT = HistoryRow.find('input[name^="Histories["][name$="].EndDT"]').val();
        var HistorySportsNo = HistoryRow.find('select[name^="Histories["][name$="].SportsNo"]').val();

        if (!HistorySchoolName || !HistoryStartDT || !HistoryEndDT || !HistorySportsNo || HistoryStartDT > HistoryEndDT) {
            alert("모든 항목을 입력하고, 근무시작일보다 근무종료일이 빠를 수 없습니다.");
            return;
        } 
        
    }

    var row = '<tr>';
    row += '<input type="hidden" id="HistoryLeaderNo" name="Histories[' + historyIndex + '].LeaderNo" value="' + LeaderNo + '"/>';
    row += '<td><input type="text" name="Histories[' + historyIndex + '].SchoolName" value="" placeholder="학교명을 입력해주세요." /></td>';
    row += '<td><input type="date" name="Histories[' + historyIndex + '].StartDT" value="" data-placeholder="근무 시작일을 선택해주세요." required aria-required="true" /></td>';
    row += '<td><input type="date" name="Histories[' + historyIndex + '].EndDT" value="" data-placeholder="근무 종료일을 선택해주세요." required aria-required="true" /></td>';
    row += '<td><select class="Test" name="Histories[' + historyIndex + '].SportsNo"><option value="" disabled selected>종목을 선택해주세요</option></select><input type="hidden" id="asdasd" name="HistorySports[' + historyIndex + ']" value="@historiesItem.SportsNo" /></td>';
    row += '<td><input type="button" class="btn btn-sm minus-btn" value="삭제" onclick="Item_Remove(this)" style="background-color: #969696"/></td></tr>';

    $("#history_table tbody").append(row);
    historyIndex++;
}

// 자격사항 테이블 추가
function addCertificate() {
    var LeaderNo = $("#LeaderNo").val();
    var row = '<tr>';
    row += '<input type="hidden" id="CertificateLeaderNo" name="Certificates[' + certificateIndex + '].LeaderNo" value="' + LeaderNo + '"/>'
    row += '<td><input type="text" name="Certificates[' + certificateIndex + '].CertificateName" value="" placeholder="자격을 입력해주세요." /></td>';
    row += '<td><input type="text" name="Certificates[' + certificateIndex + '].CertificateNo" value="" placeholder="영문, 숫자만 입력해주세요." required aria-required="true" /></td>';
    row += '<td><input type="date" name="Certificates[' + certificateIndex + '].CertificateDT" value="" data-placeholder="취득일자를 선택해주세요." required aria-required="true" /></td>';
    row += '<td><input type="text" name="Certificates[' + certificateIndex + '].Organization" value="" placeholder="발급기관을 선택해주세요." required aria-required="true" /></td>';
    row += '<td><input type="button" class="btn btn-sm minus-btn" value="삭제" onclick="Item_Remove(this)" style="background-color: #969696"/></td></tr>';

    $("#certificate_table").append(row);
    certificateIndex++;
};

// 등록하기 - POST
function addLeaderData() {
    if (validation()) {
        validAfterModal();
    } else {
        openShortModal('필수입력값 확인', '필수입력값이 채워지지 않았습니다.<br>확인 후 채워주시기 바랍니다.');
    }
    validAfterModal();

}
function validAfterModal() {
    var modal = $('#shortModal');
    modal.find('.modal-body h2').text('지도자 등록');
    modal.find('.modal-body p').text('입력한 내용으로 지도자를 등록하시겠습니까?');

    var buttons = '<input class="btn btn-secondary" type="button" value="취소" onclick="closeShortModal()" style="margin-right: 8px;">';
    buttons += '<input class="btn btn-primary" type="button" value="확인" onclick="submitForm()">';
    modal.find('.modal-body .modal-buttons').html(buttons);

    modal.modal('show');
}

function submitForm() {
    $('form').submit();
}


// 유효성 검사
function validation() {
    // 일반현황
    var leaderNo = $('#LeaderNo').val();
    var schoolNo = $('#SchoolNo').val();
    var leaderName = $('#LeaderName').val();
    var birthday = $('#BirthDay').val();
    var gender = $("input[name='Gender']:checked");
    var sportsNo = $('#SportsNo').val();
    var telNo1 = $("#TelNo").val();
    var telNo2 = $("#TelNo2").val();
    var telNo3 = $("#TelNo3").val();
    var empDT = $('#EmpDT').val()
    // 근무이력
    var historySchoolName = $('#HistoriesSchoolName').val();
    var historyStartDT = $('#HistoriesStartDT').val();
    var historyEndDT = $('#HistoriesEndDT').val();
    var historySportsNo = $('#HistoriesSportsNo').val();
    // 자격사항
    var certificateName = $('#CertificatesCertificateName').val();
    var certificateNumber = $('#CertificatesCertificateNo').val();
    var certificateDT = $('#CertificatesCertificateDT').val();
    var organization = $('#CertificatesOrganization').val();

    var check = /^[가-힣a-zA-Z]+$/
    var checkDate = /^[0-9]{4}[-]+[0-9]{2}[-]+[0-9]{2}$/;
    var checkTelNo = /^[0-9]{3}$/;
    var checkLeaderNo = /^JB[0-9]{2}[-]+[0-9]{3}$/;
    var checkSchoolNo = /^SC[0-9]{4}$/;
    var checkCertificateNumber = /^[a-zA-Z0-9]+$/;
    var checkCertificateName = /^[가-힣a-zA-Z0-9]+$/;

    if (!checkLeaderNo.test(leaderNo)) {
        console.warn("식별코드를 입력하세요.");
        return false;
    }

    if (!checkSchoolNo.test(schoolNo)) {
        console.warn("학교명을 입력하세요.");
        return false;
    }

    if (!check.test(leaderName)) {
        console.warn("성명을 입력하세요.");
        return false;
    }

    if (!checkDate.test(birthday)) {
        console.warn("생년월일을 입력하세요.");
        return false;
    }

    if (gender.length === 0) {
        console.warn("성별을 선택하세요.");
        return false;
    }

    if (!sportsNo) {
        console.warn("종목을 선택하세요.");
        return false;
    }

    if (!checkTelNo.test(telNo1) || !checkTelNo.test(telNo2) || !/^[0-9]{4}$/.test(telNo3)) {
        console.warn("근무지 전화번호를 입력하세요.\n(올바른 전화번호 형식이 아닙니다.(063-123-4567))");
        return false;
    }

    if (!checkDate.test(empDT)) {
        console.warn("최초채용일을 선택하세요.");
        return false;
    }

    if (!check.test(historySchoolName)) {
        console.log(historySchoolName);
        console.warn("근무기관을 입력하세요.");
        return false;
    }

    if (!checkDate.test(historyStartDT)) {
        console.warn("근무시작일을 입력하세요.");
        return false;
    }

    if (!checkDate.test(historyEndDT)) {
        console.warn("근무종료일을 입력하세요.");
        return false;
    }

    if (!historySportsNo) {
        console.warn("근무종목을 선택하세요.");
        return false;
    }

    if (!checkCertificateName.test(certificateName)) {
        console.warn("자격/면허를 입력하세요.");
        return false;
    }

    if (!checkCertificateNumber.test(certificateNumber)) {
        console.warn("자격번호를 입력하세요.\n(영문, 숫자만 입력해주세요.)");
        return false;
    }

    if (!checkDate.test(certificateDT)) {
        console.warn("취득일자를 선택하세요.");
        return false;
    }

    if (!check.test(organization)) {
        console.warn("발급기관을 입력하세요.");
        return false;
    }

    return true;
}

function loadSports() {
    $.ajax({
        url: '/api/sports',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $(".sports-dropdown").each(function (index, dropdown) {
                $.each(data, function (index, item) {
                    var isSelected = ($("#Sports").val() === item.sportsName)
                    var Test = ($("#TestSports").val() === item.sportsNo)
                    var option = $("<option>").attr("value", item.sportsNo).text(item.sportsName);
                    if (isSelected) {
                        option.prop("selected", true);
                    }
                    if (Test) {
                        option.prop("selected", true);
                    }

                    $(dropdown).append(option);
                });
            });
            $(".sportsdropdown").each(function (index, dropdown) {
                $.each(data, function (index, item) {
                    var option = $("<option>").attr("value", item.sportsNo).text(item.sportsName);
                    $(dropdown).append(option);
                });
            });
            $(".Test").each(function (index, dropdown) {
                $.each(data, function (index, item) {
                    var option = $("<option>").attr("value", item.sportsNo).text(item.sportsName);

                    $(dropdown).append(option);

                    var inputValue = $("#2").val;
                    var isSelected = (inputValue === item.sportsNo);

                    if (isSelected) {
                        option.prop("selected", true);
                    }
                });
            });
            $(".HistorySports").each(function (index, dropdown) {
                $.each(data, function (dataIndex, item) {
                    var option = $("<option>").attr("value", item.sportsNo).text(item.sportsName);

                    $(dropdown).append(option);

                    var inputValue = $("input[name='HistorySports[" + index + "]']").val();
                    var isSelected = (inputValue === item.sportsName);

                    if (isSelected) {
                        option.prop("selected", true);
                    }
                });
            });

        },
        error: function (error) {
            console.error('종목 데이터 오류:', error);
        }
    });
}


function loadModalTable(url, tbodySelector, modalId) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var tableBody = $(tbodySelector);
            tableBody.empty(); 
            $.each(data, function (index, item) {
                var row = $("<tr>").on('click', function () {
                    selectRow(this);
                });

                $.each(item, function (key, value) {
                    row.append($("<td>").text(value));
                });

                tableBody.append(row);
            });

        },
        error: function (error) {
            console.error('데이터 오류', error);
        }
    });
}

function updateLeader() {
    var form = $('form');
    form.submit();
}

