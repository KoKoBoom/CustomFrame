/// <reference path="../jquery-1.10.2.js" />
/**
         * 设置未来(全局)的AJAX请求默认选项
         * 主要设置了AJAX请求遇到Session过期的情况
         *
         * 配置捕获全局 Ajax 请求的 完成 和 错误 事件提供统一的提示
         */
jQuery.ajaxSetup({
    //type: 'POST',
    dataType: "json",
    //contentType: "application/json; charset=utf-8",
    complete: function (xhr, status) {
        var sessionStatus = xhr.getResponseHeader('sessionstatus');
        if (sessionStatus == 'timeout') {
            var top = getTopWinow();
            $.messager.confirm("操作提示", '由于您长时间没有操作, session已过期, 请重新登录！', function (data) {
                if (data) {
                    top.location.href = '/account/login';
                }
            });
        }
    },
    error: function (jqXHR, textStatus, errorMsg) {
        if (jqXHR.responseJSON) {
            alert(jqXHR.responseJSON.ErrorMessage);
        } else if (jqXHR.responseText) {
            alert(jQuery.parseJSON(jqXHR.responseText).ErrorMessage);
        } else if (errorMsg) {
            alert(errorMsg);
        }
        //$.messager.show({
        //    title: '提示',
        //    msg: "<div style='text-align:center'>网络或服务器故障！</div>",
        //    showType: 'slide'
        //});
    }
});