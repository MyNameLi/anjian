/// <reference path="template.js" />
(function () {
    var root = this;
    var EventCenter;
    if (typeof exports !== 'undefined') {
        EventCenter = exports;
    } else {
        EventCenter = root.EventCenter = {};
    }
    EventCenter.id = "1";
    EventCenter.isNewEvents = false;
    //Post Jons 请求 url地址，data：发送数据,callbackFun:请求完成回调函数
    EventCenter.getPostJson = function (url, data, callbackFun) { //
        $.post(url, data, function (data) {
            if (typeof (callbackFun) == "function")
                callbackFun(data);
        }, "json");
    }
    //initEvent
    EventCenter.eventHander = "../Handler/EventsHandler.ashx";
    EventCenter.eventUrlData = { "queryact": "querystr", "act": "initEvent", "eventid": EventCenter.id };
    //initTopic
    EventCenter.topicHander = "../Handler/EventsHandler.ashx";
    EventCenter.topicUrlData = { "queryact": "querystr", "act": "initTopic", "eventid": EventCenter.id };
    //initClue
    EventCenter.clueHander = "../Handler/EventsHandler.ashx";
    EventCenter.clueUrlData = { "queryact": "querystr", "act": "initClue", "eventid": EventCenter.id };
    //initImg
    EventCenter.imgHander = "../Handler/EventsHandler.ashx";
    EventCenter.imgUrlData = { "queryact": "querystr", "act": "initImg", "eventid": EventCenter.id };

    /*====================================
    *Event表操作
    *=======================================
    */
    EventCenter.initEvent = function () {
        function _callback(data) {
            if (data && data.success == "1") {
                $("#txt_event_name").val(unescape(data.data.eventname));
                $("#txtEventsTime").val(unescape(data.data.eventtime));
                $("#txtEventsSummary").val(unescape(data.data.summary));
                $("#txtEventsKeywords").val(unescape(data.data.keywords));
                $("#btnEvents").unbind("click").bind("click", { "data": data.data }, updateEvent);
            } else {
                $("#btnEvents").unbind("click").bind("click", addEvent);
            }
        }
        function updateEvent(datas) {
            var model = datas.data.data;
            EventCenter.eventUrlData["act"] = "updateEvent";
            EventCenter.eventUrlData["ID"] = model.id;
            EventCenter.eventUrlData["eventid"] = model.eventid;
            EventCenter.eventUrlData["eventName"] = $("#txt_event_name").val();
            EventCenter.eventUrlData["eventTime"] = $("#txtEventsTime").val();
            EventCenter.eventUrlData["summary"] = $("#txtEventsSummary").val();
            EventCenter.eventUrlData["keyword"] = $("#txtEventsKeywords").val();
            EventCenter.getPostJson(EventCenter.eventHander, EventCenter.eventUrlData, function (data) {
                if (data.success == "1") {
                    alert("修改成功！");
                    EventCenter.eventUrlData["act"] = "initEvent";
                }
            });
        }
        function addEvent() {
            EventCenter.eventUrlData["act"] = "addEvent";
            EventCenter.eventUrlData["ID"] = "0";
            EventCenter.eventUrlData["eventName"] = $("#txt_event_name").val();
            EventCenter.eventUrlData["eventTime"] = $("#txtEventsTime").val();
            EventCenter.eventUrlData["summary"] = $("#txtEventsSummary").val();
            EventCenter.eventUrlData["keyword"] = $("#txtEventsKeywords").val();
            EventCenter.getPostJson(EventCenter.eventHander, EventCenter.eventUrlData, function (data) {
                if (data.success == "1") {
                    EventCenter.eventUrlData["act"] = "initEvent";
                    alert("添加成功！");
                }
            });
        }
        EventCenter.eventUrlData["eventid"] = EventCenter.id;
        EventCenter.getPostJson(EventCenter.eventHander, EventCenter.eventUrlData, _callback);
    }
    /*==================================
    *Topic表操作
    *===================================
    */
    EventCenter.initTopic = function () {
        EventCenter.topicUrlData["eventid"] = EventCenter.id;
        ready();
        function _callback(data) {
            $("#tbdTopic").empty();
            if (data && data.length > 0) {
                for (var i = 0, j = data.length; i < j; i++) {
                    topicView(data[i]);
                }
            } else {
                //无数据
            }
        }
        function topicView(data) {
            for (var item in data) {
                data[item] = unescape(data[item]);
            }
            var inHtml = template.render("topic_template", data);
            var el = $(inHtml);
            $(el).find(".update").bind("click", [data, el], _updateTopic);
            $(el).find(".del").bind("click", [data, el], deletTopic);
            $("#tbdTopic").append(el);
        }
        function _updateTopic(data) {
            var model = data.data[0];
            $("#txttopicTitle").val(model.TopicTitle);
            $("#txttopicContent").val(model.TopicContent);
            $("#btnTopic").unbind("click").bind("click", [model], updateTopic);
        }
        function updateTopic(data) {
            var model = data.data[0];
            var docId = model.TopicDocid == "" ? "0" : model.TopicDocid;
            EventCenter.topicUrlData["ID"] = model.TopicId;
            EventCenter.topicUrlData["act"] = "updateEventTopic";
            EventCenter.topicUrlData["title"] = $("#txttopicTitle").val();
            EventCenter.topicUrlData["topicContent"] = $("#txttopicContent").val();
            EventCenter.topicHander["topicImage"] = "";
            EventCenter.topicUrlData["docId"] = docId;
            EventCenter.getPostJson(EventCenter.topicHander, EventCenter.topicUrlData, function (data) {
                if (data.success == "1") {
                    alert("修改成功!");
                    clear();
                    EventCenter.topicUrlData["act"] = "initTopic";
                    ready();
                }
            });
        }
        function deletTopic(data) {
            if (!confirm("确定删除吗?")) {
                return;
            }
            var model = data.data[0];
            var el = $(data.data[1]).remove();
            EventCenter.topicUrlData["act"] = "delEventTopic";
            EventCenter.topicUrlData["ID"] = model.TopicId;
            EventCenter.getPostJson(EventCenter.topicHander, EventCenter.topicUrlData, function (data) {
                if (data.success == "1") {
                    $(el).remove();
                    EventCenter.topicUrlData["act"] = "initTopic";
                    alert("删除成功！");
                }
            });
        }
        function addTopic() {
            EventCenter.topicUrlData["ID"] = "0";
            EventCenter.topicUrlData["act"] = "addEventTopic";
            EventCenter.topicUrlData["title"] = $("#txttopicTitle").val();
            EventCenter.topicUrlData["topicContent"] = $("#txttopicContent").val();
            EventCenter.topicHander["topicImage"] = "";
            EventCenter.topicUrlData["docId"] = "0";
            EventCenter.getPostJson(EventCenter.topicHander, EventCenter.topicUrlData, function (data) {
                if (data.success == "1") {
                    alert("添加成功!");
                    EventCenter.topicUrlData["act"] = "initTopic";
                    clear();
                    ready();
                } else {
                    alert("添加失败！");
                }
            });
        }
        $("#btnTopic").unbind("click").bind("click", addTopic);
        $("#btn_addTopic").unbind("click").bind("click", function () {
            clear();
            $("#btnTopic").unbind("click").bind("click", addTopic);
        });
        function clear() {
            $("#txttopicTitle").val("");
            $("#txttopicContent").val("");
        }
        function ready() {
            EventCenter.getPostJson(EventCenter.topicHander, EventCenter.topicUrlData, _callback);
        }
    }
    /*======================================
    *Clue表操作
    *=======================================
    */
    EventCenter.initClue = function () {
        EventCenter.clueUrlData["eventid"] = EventCenter.id;
        ready();
        function _callback(data) {
            $("#tbdClue").empty();
            if (data && data.length > 0) {
                for (var i = 0, j = data.length; i < j; i++) {
                    clueView(data[i]);
                }
            } else {
                //无数据//$("#btnClue").unbind("click").bind("click", addClue);
            }
        }
        function clueView(data) {
            for (var item in data) {
                data[item] = unescape(data[item]);
            }
            var inHtml = template.render("clue_template", data);
            var el = $(inHtml);
            $(el).find(".update").bind("click", [data], _updateClue);
            $(el).find(".del").bind("click", [data, el], deletClue);
            $("#tbdClue").append(el);
        }
        function _updateClue(data) {
            var model = data.data[0];
            $("#txtClueTime").val(model.ClueTime);
            $("#txtClueTitle").val(model.ClueTitle);
            $("#btnClue").unbind("click").bind("click", model, updateClue);
        }
        function updateClue(data) {
            var model = data.data;
            EventCenter.clueUrlData["ID"] = model.ClueId;
            EventCenter.clueUrlData["act"] = "updateEventClue"; // model.ClueId;
            EventCenter.clueUrlData["docId"] = model.ClueDocid == "" ? "0" : model.ClueDocid;
            EventCenter.clueUrlData["title"] = $("#txtClueTitle").val();
            EventCenter.clueUrlData["clueTime"] = $("#txtClueTime").val();
            EventCenter.getPostJson(EventCenter.clueHander, EventCenter.clueUrlData, function (data) {
                if (data.success == "1") {
                    alert("修改成功！");
                    EventCenter.clueUrlData["act"] = "initClue";
                    clear();
                    ready();
                }
            });
        }
        function deletClue(data) {
            if (!confirm("确定删除吗?")) {
                return;
            }
            var model = data.data[0];
            var el = data.data[1];
            EventCenter.clueUrlData["ID"] = model.ClueId;
            EventCenter.clueUrlData["act"] = "delEventClue";
            EventCenter.getPostJson(EventCenter.clueHander, EventCenter.clueUrlData, function (data) {
                if (data.success == "1") {
                    $(el).remove();
                    EventCenter.clueUrlData["act"] = "initClue";
                }
            });
        }
        function addClue() {
            EventCenter.clueUrlData["ID"] = "0";
            EventCenter.clueUrlData["act"] = "addEventClue";
            EventCenter.clueUrlData["docId"] = "0";
            EventCenter.clueUrlData["title"] = $("#txtClueTitle").val();
            EventCenter.clueUrlData["clueTime"] = $("#txtClueTime").val();
            EventCenter.getPostJson(EventCenter.clueHander, EventCenter.clueUrlData, function (data) {
                if (data.success == "1") {
                    alert("添加成功！");
                    EventCenter.clueUrlData["act"] = "initClue";
                    clear();
                    ready();
                }
            });
        }
        function clear() {
            $("#txtClueTitle").val("");
            $("#txtClueTime").val("");
        }
        $("#btnClue").unbind("click").bind("click", addClue);
        $("#btn_addClue").unbind("click").bind("click", function () {
            clear();
            $("#btnClue").unbind("click").bind("click", addClue);
        });
        function ready() {
            EventCenter.getPostJson(EventCenter.clueHander, EventCenter.clueUrlData, _callback);
        }
    }
    /*=========================================
    *Img表操作
    *==========================================
    */
    EventCenter.initImg = function () {
        EventCenter.imgUrlData["eventid"] = EventCenter.id;
        rendy();
        function _callback(data) {
            $("#tbdImg").empty();
            if (data && data.length > 0) {
                for (var i = 0, j = data.length; i < j; i++) {
                    clueView(data[i]);
                }
            } else {
                //无数据//$("#btnImg").unbind("click").bind("click", addImg);
            }
        }
        function clueView(data) {
            for (var item in data) {
                data[item] = unescape(data[item]);
            }
            var inHtml = template.render("img_template", data);
            var el = $(inHtml);
            $(el).find(".update").bind("click", [data], _updateImg);
            $(el).find(".del").bind("click", [data, el], _delImg);
            $("#tbdImg").append(el);
        }
        function _updateImg(data) {
            var model = data.data[0];
            $("#txt_imgpath").val(model.ImgPath);
            $("#txt_imgurl").val(model.ImgUrl);
            if (model.ImgType == "1") {
                $("#rd_maxImg").attr("checked", true);
            } else if (model.ImgType == "0") {
                $("#rd_minImg").attr("checked", true);
            } else {
                $("#rd_backgroundImg").attr("checked", true);
            }
            $("#txt_imgalt").val(model.ImgTitle);
            $("#btnImg").unbind("click").bind("click", model, updateImg);
        }
        function updateImg(data) {
            var model = data.data;
            EventCenter.imgUrlData["act"] = "updateEventImg";
            EventCenter.imgUrlData["ID"] = model.ImgId;
            EventCenter.imgUrlData["imgPath"] = $("#txt_imgpath").val();
            EventCenter.imgUrlData["imgUrl"] = $("#txt_imgurl").val();
            EventCenter.imgUrlData["imgType"] = $("input[name=rdImgType]:checked").val();
            EventCenter.imgUrlData["imgAlt"] = $("#txt_imgalt").val();
            EventCenter.getPostJson(EventCenter.imgHander, EventCenter.imgUrlData, function (data) {
                if (data.success == "1") {
                    alert("修改成功！");
                    EventCenter.imgUrlData["act"] = "initImg";
                    clear();
                    rendy();
                }
            });
        }
        function _delImg(data) {
            if (!confirm("确定删除吗?")) {
                return;
            }
            var model = data.data[0];
            var el = data.data[1];
            EventCenter.imgUrlData["ID"] = model.ImgId;
            EventCenter.imgUrlData["act"] = "delEventImg";
            EventCenter.getPostJson(EventCenter.imgHander, EventCenter.imgUrlData, function (data) {
                if (data.success == "1") {
                    EventCenter.clueUrlData["act"] = "initImg";
                    $(el).remove();
                }
            });
        }
        function addImg() {
            EventCenter.imgUrlData["act"] = "addEventImg";
            EventCenter.imgUrlData["ID"] = "0";
            EventCenter.imgUrlData["imgPath"] = $("#txt_imgpath").val();
            EventCenter.imgUrlData["imgUrl"] = $("#txt_imgurl").val();
            EventCenter.imgUrlData["imgType"] = $("input[name=rdImgType]:checked").val();
            EventCenter.imgUrlData["imgAlt"] = $("#txt_imgalt").val();
            EventCenter.getPostJson(EventCenter.imgHander, EventCenter.imgUrlData, function (data) {
                if (data.success == "1") {
                    alert("添加成功！");
                    EventCenter.imgUrlData["act"] = "initImg";
                    clear();
                    rendy();
                }
            });
        }
        function clear() {
            $("#txt_imgpath").val("");
            $("#txt_imgurl").val("");
            $("#txt_imgalt").val("");
        }
        $("#btnImg").unbind("click").bind("click", addImg);
        $("#btn_addImg").unbind("click").bind("click", function () {
            clear();
            $("#btnImg").unbind("click").bind("click", addImg);
        })
        function rendy() {
            EventCenter.getPostJson(EventCenter.imgHander, EventCenter.imgUrlData, _callback);
        }
    }

    EventCenter.init = function () {
        //EventCenter.id = eid;
        EventCenter.initEvent();
        EventCenter.initTopic();
        EventCenter.initClue();
        EventCenter.initImg();
    }
}).call(this);

$(function () {
    var reg = /^\d+$/;
    var eventId = $.query.get("eventid");
    if (reg.test(eventId)) {
        EventCenter.id = eventId;
        EventCenter.init();
    }
    //    EventCenter.uploadImg();
});