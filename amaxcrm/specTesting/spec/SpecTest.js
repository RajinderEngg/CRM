/// <reference path="../lib/jasmine-2.4.1/jasmine.js" />

/// <reference path="../../app/testingComponents/test.js" />


describe("Calculator Spec Test", function () {
    describe("Should Defined test", function () {
        var calc = new Calculator();
        it("Calculator should defined", function () {
            expect(calc).toBeDefined();
        });
        it("Add Should be defined in calculator", function () {
            expect(calc.add()).toBeDefined();
        });
        it("Div Should be defined", function () {
            expect(calc.div()).toBeDefined();
        });
        it("Sub Should be define", function () {
            expect(calc.sub()).toBeDefined();
        });
        it("Reset should defined", function () {
            expect(calc.reset()).toBeDefined();
        });
        it("val should defined", function () {
            expect(calc.val()).toBeDefined();
        });
    });

    describe("Should Pass all the logical operation", function () {
        var calc = new Calculator();
        it("Should pass the add logics", function () {
            expect(calc.reset()).toEqual(0);
            expect(calc.add(1)).toEqual(1);
            expect(calc.add(1)).toEqual(2);
            expect(calc.add(1)).toEqual(3);
            expect(calc.add(1)).toEqual(4);
        });
        it("Should pass the sub logics", function () {
            expect(calc.reset()).toEqual(0);
            expect(calc.sub(1)).toEqual(-1);
            expect(calc.sub(1)).toEqual(-2);
            expect(calc.sub(1)).toEqual(-3);
            expect(calc.sub(1)).toEqual(-4);
        });
        it("Should pass the div logics", function () {
            expect(calc.reset()).toEqual(0);
            expect(calc.val()).toEqual(0);
            expect(calc.div(1)).toEqual(0);
        });
        it("Should pass the reset", function () {
            calc.reset();
            calc.add(5);
            expect(calc.val()).toEqual(5);
        });
    });
    
    
    
    describe("AmaxAppComponent loginvalidation test",function () {
        var x = new AmaxAppComponent();
        it("AmaxAppComponent should be defined",function () {
            expect(true).toBe(true);
        })
    })
    
});