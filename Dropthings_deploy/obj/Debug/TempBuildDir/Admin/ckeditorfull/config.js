/**
* @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
* For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    //config.uiColor = '#AADC6E';

    //界面的语言配置 设置为'zh-cn'即可
    //config.defaultLanguage = 'uk';
    config.language = "zh-cn";
    //编辑器的高度
    config.height = 400;
    //编辑器的宽度 plugins/undo/plugin.js
    //config.width = 800;
    //可选界面包
    //config.skin = 'default';
    //使用的工具栏 plugins/toolbar/plugin.js 当然你也可以使用自己的工具栏
    //config.toolbar = "Full";
    //这将配合：
    //    alert("ok");
    //    config.toolbar_Full = [
    //    [ '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
    //    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
    //    ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
    //    ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
    //    '/',
    //    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    //    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
    //    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    //    ['Link', 'Unlink', 'Anchor'],
    //    ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
    //    '/',
    //    ['Styles', 'Format', 'Font', 'FontSize'],
    //    ['TextColor', 'BGColor']
    //                ];

    //    //选择界面功能
    //    config.toolbar = 'Buaa';
    //    //定义界面工具
    //    config.toolbar_Buaa = [
    //            ['-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
    //            ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
    //            ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
    //            ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
    //            '/',
    //            ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    //            ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
    //            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    //            ['Link', 'Unlink', 'Anchor'],
    //            ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
    //            '/',
    //            ['Styles', 'Format', 'Font', 'FontSize'],
    //            ['TextColor', 'BGColor']
    //        ];

    //config.toolbar = 'office2003';


    config.filebrowserBrowseUrl = '../ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '../ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '../ckfinder/ckfinder.html?Type=Flash';
    //config.filebrowserUploadUrl: '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '../ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
