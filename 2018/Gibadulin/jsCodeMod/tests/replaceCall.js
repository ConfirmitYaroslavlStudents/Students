const replaceCall = require('..\\modules\\replaceCall');
const assert = require('assert');

/*it('test name', function(){
  const fileSource    ;
  const oldCallSource ;
  const newCallSource ;
  const expected      ;

  const actual = replaceCall(fileSource, oldCallSource, newCallSource);

  assert.equal(actual, expected);
});*/

describe('Functional tests for replaceCall',function () {
  it('zero example', function(){
    const fileSource    = "api.showSurvey('inviteParam', 'overlayParam', 'id');";
    const oldCallSource = "api.showSurvey('inviteParam', 'overlayParam', 'id');";
    const newCallSource = "api.invite('inviteParam').overlay('overlayParam').survey('id').show();";
    const expected      = "api.invite('inviteParam').overlay('overlayParam').survey('id').show();";

    const actual = replaceCall(fileSource, oldCallSource, newCallSource);

    assert.equal(actual, expected);
  });

  describe('Semicolon', function () {
    it('one semicolon at the end', function(){
      const fileSource    = 'a;';
      const oldCallSource = 'a;';
      const newCallSource = 'b';
      const expected      = 'b;';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('call VS call;', function(){
      const fileSource = 'a; a\na\na;';
      const oldCalls   = ['a;', 'a'];
      const newCalls   = ['b;', 'b'];
      const expected   = 'b;\nb;\nb;\nb;';

      let actual;
      for(let i=0; i<2; i++)
        for(let j=0; j<2; j++){
          actual = replaceCall(fileSource, oldCalls[i], newCalls[j]);
          assert.equal(actual, expected);
        }
    });
  });

  describe('Some simple examples', function () {
    it('a -> b', function(){
      const fileSource    = 'a; a.a; a.b; a(); a().a; a().b;';
      const oldCallSource = 'a;';
      const newCallSource = 'b;';
      const expected      = 'b;\nb.a;\nb.b;\nb();\nb().a;\nb().b;';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('a -> a()', function(){
      const fileSource    = 'a; a.a; a.b; a(); a().a; a().b;';
      const oldCallSource = 'a;';
      const newCallSource = 'a();';
      const expected      = 'a();\na().a;\na().b;\na()();\na()().a;\na()().b;';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('a -> a.a', function(){
      const fileSource    = 'a; a.a; a.b; a(); a().b;';
      const oldCallSource = 'a;';
      const newCallSource = 'a.a;';
      const expected      = 'a.a;\na.a.a;\na.a.b;\na.a();\na.a().b;';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('a() -> a.a()', function(){
      const fileSource    = 'a(); a().a; a;';
      const oldCallSource = 'a();';
      const newCallSource = 'a.a();';
      const expected      = 'a.a();\na.a().a;\na;';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });
  });

  describe('Required call...', function () {
    it('have properties', function(){
      const fileSource    = 'a(); a().prop; a().call(); a()();';
      const oldCallSource = 'a();';
      const newCallSource = 'b();';
      const expected      = 'b();\nb().prop;\nb().call();\nb()();';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('is property (1)', function(){
      const fileSource    = 'obj.a(); x().a(); a();';
      const oldCallSource = 'a();';
      const newCallSource = 'b();';
      const expected      = 'obj.a();\nx().a();\nb();';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('is property (2)', function(){
      const fileSource    = 'obj.a; x().a; y().a(); a();';
      const oldCallSource = 'a;';
      const newCallSource = 'b();';
      const expected      = 'obj.a;\nx().a;\ny().a();\nb()();';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('is property (3)', function(){
      const fileSource    = 'obj.a; x().a; y().a(); a();';
      const oldCallSource = 'a;';
      const newCallSource = 'b;';
      const expected      = 'obj.a;\nx().a;\ny().a();\nb();';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('in block', function () {
      const fileSource    = 'function add(){a();}';
      const oldCallSource = 'a();';
      const newCallSource = 'b();';
      const expected      = 'function add() {\n\tb();\n}';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });
  });

  describe('Play with arguments', function () {
    it('without args', function(){
      const fileSource    = 'a(); b(); c();';
      const oldCallSource = 'b();';
      const newCallSource = 'b.show();';
      const expected      = 'a();\nb.show();\nc();';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('one argument', function(){
      const fileSource    = 'a(param1); a(); a(param2, param3)';
      const oldCallSource = "a('p')";
      const newCallSource = "a.addArg('p')()";
      const expected      = 'a.addArg(param1)();\na();\na(param2, param3);';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('permutation of arguments', function(){
      const fileSource    = 'show(1, 2, 3, 4, 5);';
      const oldCallSource = "show('p1', 'p2', 'p3', 'p4', 'p5')";
      const newCallSource = "show('p3', 'p1', 'p5', 'p2', 'p4')";
      const expected      = 'show(3, 1, 5, 2, 4);';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('two suitable calls and two almost suitable call(test args collecting)', function(){
      const fileSource    = 'a(p1).b(p2); a(p3).c(p4); a(p5).b; a(p6).b(p7);';
      const oldCallSource = "a('p').b('q');";
      const newCallSource = "ab('p', 'q');";
      const expected      = 'ab(p1, p2);\na(p3).c(p4);\na(p5).b;\nab(p6, p7);';

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });
  });

  describe('Empty parametrs:', function () {
    it('fileSource', function(){
      const fileSource    = '';
      const oldCallSource = 'a;';
      const newCallSource = 'b;';
      const expected      = fileSource;

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('oldCallSource', function(){
      const fileSource    = 'a;';
      const oldCallSource = '';
      const newCallSource = 'b;';
      const expected      = fileSource;

      const actual = replaceCall(fileSource, oldCallSource, newCallSource);

      assert.equal(actual, expected);
    });

    it('newCallSource', function(){
      const fileSource    = 'c;';
      const oldCallSource = 'a;';
      const newCallSource = '';
      const expected      = new Error("Body of newCallSource is empty");;

      const actual = function () {
        replaceCall(fileSource, oldCallSource, newCallSource);
      };

      assert.throws(actual, expected);
    });
  });
});
