const puppeteer = require ('puppeteer');
const { expect } = require('chai');


const sso = 'https://kfc-sso.com/#/login';
const myAPMessenger = 'https://www.myacademicpyramid.com/#/Chat';
const validEmail = 'nguyentrong56@icloud.com';
const validPassword = 'Trangdao2093';
const validReceiverUsername="trangdao2093@gmail.com";
const validMessage = "Hello World";
const TYPE_DELAY = 0;

function timeout(ms){
	return new Promise(resolve => setTimeout(resolve, ms));
};
async function LoginValid(){
	const browser = await puppeteer.launch(browser);
	const page = await browser.newPage();
	
	
};

describe('messenger',() => {
	let browser 

	before(async () => {
		browser = await puppeteer.launch({headless : false})
	})
	after(async () =>{
		await browser.close()
	})

	describe('send message successfully ', () =>{
		let page
		before(async () => {
			page = await browser.newPage();
			await page.goto(sso);
			await page.waitForSelector('main h1');
			await page.type('#email', validEmail, {delay: TYPE_DELAY});
			await page.type('#password', validPassword,{delay: TYPE_DELAY});
			let MyApSelector = '.h3 .strong :contains(My Academic Pyramid)';
			await page.waitForSelector('main h1');
			await page.click(MyApSelector);
			//await page.goto(myAPMessenger);
		})

		after(async () =>{
			await page.close()
		})

		beforeEach(async() => {
			await page.reload()
			await page.waitForSelector('main' )
		})

		it('show a new conversation with new message', async () =>{
			await page.click('#newMsgButton', {delay:TYPE_DELAY})
			await page.type('#receiverUsername', validReceiverUsername, {delay:TYPE_DELAY})
			await page.type('#inputMessage', validMessage, {delay:TYPE_DELAY} )

			await page.click ('#sendNewMsgButton')
			let username = await page.$('usernameChip')
			let message = await page.$('messageChip')

			expect(username).to.not.be.null

			expect(message).to.not.be.null
			expect(await (await username.getProperty('textContent')).jsonValue()).to.equal(validReceiverUsername)

			expect(await (await message.getProperty('textContent')).jsonValue()).to.equal('Hello World')

		})

		

		

	
		

	})



})

