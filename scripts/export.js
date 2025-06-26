import { promises as fs } from 'fs'
// import * as theme from 'jsonresume-theme-even'
import * as theme from 'jsonresume-theme-local-onepageresume'
// import * as theme from 'jsonresume-theme-onepageresume'
// import * as theme from 'jsonresume-theme-onepageresumefixed'
// import * as theme from 'jsonresume-theme-onepageresume-alt'
import puppeteer from 'puppeteer'
import { render } from 'resumed'

const resume = JSON.parse(await fs.readFile('resume.sv.json', 'utf-8'))
const html = await render(resume, theme)

await fs.writeFile('resume.sv.html', html)

const browser = await puppeteer.launch()
const page = await browser.newPage()

await page.setContent(html, { waitUntil: 'networkidle0' })
await page.pdf({
    path: 'resume.sv.pdf',
    format: 'a4',
    printBackground: true,
    preferCSSPageSize: true})
await browser.close()