import Base from './base/base';

export default class Person {
    constructor(name, age) {
        this.name = name
        this.age = age
    }

    speak() {
        console.log(
            `Hi I'm ${this.name} and ${this.age} years old and I am awesome`
        );
        let b = new Base('Mawwwww');
        alert(this.name + ' ' + b.getName());
    }
}