jscodeshift -t js-code-mod\transforms\change-call-function-style.js <filesPath> <optionsPath>

<filesPath> - path to the file or directory to be transformed
<optionsPath> - path to the file containing --old and --new options

example:
        jscodeshift -t js-code-mod\transforms\replace-code.js .\js-code-mod\forTesting\forTest.js --optPath .\js-code-mod\forTesting\options.txt
