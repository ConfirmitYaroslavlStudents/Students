/*
INPUT:
show();
var x = 10;
show(fr, merq);
show(q, e);

OUTPUT:
show();
var x = 10;
show.must(fr).go(merq).on();
show.must(q).go(e).on();
*/
show();
var x = 10;
show(fr, x == 10);
show(q, e);
