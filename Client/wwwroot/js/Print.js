function printDiv(divId) {
    var divContents = document.getElementById(divId).innerHTML;
    var printWindow = window.open('', '', 'height=600,width=800');
    printWindow.document.write('<html><head><title>Print DIV</title>');
    printWindow.document.write('</head><body>');
    printWindow.document.write(divContents);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
}
function printDataToPrinter(fullName, reservationDate, doc_no, reservationHour) {
    var iframe = document.createElement('iframe');
    iframe.style.position = 'absolute';
    iframe.style.width = '0';
    iframe.style.height = '0';
    iframe.style.border = 'none';
    document.body.appendChild(iframe);

    var doc = iframe.contentWindow.document;

    doc.open();
    doc.write('<html dir="rtl" style="font-family:cairo" ><body>');
    doc.write('<div style="text-align: center;"><img src="/Coat_of_arms_of_Iraq.svg" alt="Logo" style="width:100px;height:100px;"></div>');
    doc.write('<h3 style="text-align: center;">مكتب هويات المنطقة الخضراء</h3>');
    doc.write('<h3 style="text-align: center;">الحجز المقابلة الأمنية</h3>');
    doc.write('<div style="margin-right:20px;">');
    doc.write('<p><strong>الأسم الكامل : </strong> ' + fullName + '</p>');
    doc.write('<p><strong>رقم الحفظ :</strong> ' + doc_no + '</p>');
    doc.write('<p><strong>تاريخ الحجز : </strong> ' + reservationDate + '</p>');
    doc.write('<p><strong>ساعة الحجز : </strong> ' + reservationHour + '</p>');
    doc.write('<div>');

    doc.write('</body></html>');
    doc.close();

    iframe.contentWindow.focus();
    iframe.contentWindow.print();

    setTimeout(function () {
        document.body.removeChild(iframe);
    }, 1000);
}