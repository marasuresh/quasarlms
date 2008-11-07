// JScript File
// JSOO - Ayuda a la orientación a objetos con JS

Function.prototype.DeriveFrom = function (fnSuper) {
    var prop;
    if (this == fnSuper) {
        alert("Error - cannot derive from self");
        return;
    }
    for (prop in fnSuper.prototype) {
        if (typeof fnSuper.prototype[prop] == "function" &&
            !this.prototype[prop]) {
            this.prototype[prop] = fnSuper.prototype[prop];
        }
    }
    this.prototype[fnSuper.StName()] = fnSuper;
}
Function.prototype.StName = function () {
    var st;
    st = this.toString();
    st = st.substring(st.indexOf(" ") + 1, st.indexOf("("));
    if (st.charAt(0) == "(") {
        st = "function ...";
    }
    return st;
}
Function.prototype.Override = function (fnSuper, stMethod) {
    this.prototype[fnSuper.StName() + "_" + stMethod] = fnSuper.prototype[stMethod];
}