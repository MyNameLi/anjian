/**
* @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
* For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    //config.uiColor = '#AADC6E';

    //������������� ����Ϊ'zh-cn'����
    //config.defaultLanguage = 'uk';
    config.language = "zh-cn";
    //�༭���ĸ߶�
    config.height = 400;
    //�༭���Ŀ�� plugins/undo/plugin.js
    //config.width = 800;
    //��ѡ�����
    //config.skin = 'default';
    //ʹ�õĹ����� plugins/toolbar/plugin.js ��Ȼ��Ҳ����ʹ���Լ��Ĺ�����
    //config.toolbar = "Full";
    //�⽫��ϣ�
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

    //    //ѡ����湦��
    //    config.toolbar = 'Buaa';
    //    //������湤��
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
