﻿/*
 Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.md or http://ckeditor.com/license
*/
CKEDITOR.dialog.add("anchor", function (c) {
    function d(a, b) { return a.createFakeElement(a.document.createElement("a", { attributes: b }), "cke_anchor", "anchor") } return {
        title: c.lang.link.anchor.title, minWidth: 300, minHeight: 60, onOk: function () {
            var a = CKEDITOR.tools.trim(this.getValueOf("info", "txtName")), a = { id: a, name: a, "data-cke-saved-name": a }; if (this._.selectedElement) this._.selectedElement.data("cke-realelement") ? (a = d(c, a), a.replace(this._.selectedElement), CKEDITOR.env.ie && c.getSelection().selectElement(a)) :
                this._.selectedElement.setAttributes(a); else { var b = c.getSelection(), b = b && b.getRanges()[0]; b.collapsed ? (a = d(c, a), b.insertNode(a)) : (CKEDITOR.env.ie && 9 > CKEDITOR.env.version && (a["class"] = "cke_anchor"), a = new CKEDITOR.style({ element: "a", attributes: a }), a.type = CKEDITOR.STYLE_INLINE, c.applyStyle(a)) }
        }, onHide: function () { delete this._.selectedElement }, onShow: function () {
            var a = c.getSelection(), b = a.getSelectedElement(), d = b && b.data("cke-realelement"), e = d ? CKEDITOR.plugins.link.tryRestoreFakeAnchor(c, b) : CKEDITOR.plugins.link.getSelectedLink(c);
            if (e) { this._.selectedElement = e; var f = e.data("cke-saved-name"); this.setValueOf("info", "txtName", f || ""); !d && a.selectElement(e); b && (this._.selectedElement = b) } this.getContentElement("info", "txtName").focus()
        }, contents: [{ id: "info", label: c.lang.link.anchor.title, accessKey: "I", elements: [{ type: "text", id: "txtName", label: c.lang.link.anchor.name, required: !0, validate: function () { return this.getValue() ? !0 : (alert(c.lang.link.anchor.errorName), !1) } }] }]
    }
});