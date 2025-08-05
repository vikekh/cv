function getIconClass(network) {
    switch(network.toLowerCase()) {
			case 'github':
				return 'fab fa-github';
			case 'json resume':
				return 'far fa-file';
			case 'linkedin':
				return 'fab fa-linkedin';
		}
}

exports.render = (resume) => `
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">

<head>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

     <title>${resume.basics.name}</title>

     <style type="text/css">
        * { margin: 0; padding: 0; }
        body { font: 16px Helvetica, Sans-Serif; line-height: 24px; background: url(themes/onepageresume/images/noise.jpg); }
        .clear { clear: both; }
        #page-wrap { width: 800px; margin: 40px auto 60px; }
        #pic { float: right; margin: -30px 0 0 0; }
        h1 { margin: 0 0 16px 0; padding: 0 0 16px 0; font-size: 42px; font-weight: bold; letter-spacing: -2px; border-bottom: 1px solid #999; }
        h2 { font-size: 20px; margin: 0 0 6px 0; position: relative; }
        h2 span { position: absolute; bottom: 0; right: 0; font-style: italic; font-family: Georgia, Serif; font-size: 16px; color: #999; font-weight: normal; }
        p { font-family: Georgia; margin: 0 0 16px 0; }
        a { color: #999; text-decoration: none; border-bottom: 1px dotted #999; }
        a:hover { border-bottom-style: solid; color: black; }
        ul { margin: 0 0 32px 17px; font-family: Georgia; }
        #objective { width: 500px; float: left; }
        #objective p { font-family: Georgia, Serif; font-style: italic; color: #666; }
        dt { font-style: italic; font-weight: bold; font-size: 18px; text-align: right; padding: 0 26px 0 0; width: 150px; float: left; height: 100px; border-right: 1px solid #999;  }
        dd { width: 600px; float: right; }
        dd.clear { float: none; margin: 0; height: 15px; }
        .item { break-inside: avoid;}
        .icon .icon-text { font: 16px Helvetica, Sans-Serif; line-height: 24px; }
        a .icon { color: #000 !important; text-decoration: none; }
     </style>
     <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css" rel="stylesheet">
</head>

<body>

    <div id="page-wrap">
    
        <img src="${resume.basics.image}" alt="${resume.basics.name}" id="pic" />
    
        <div id="contact-info" class="vcard">
        
            <!-- Microformats! -->
        
            <h1 class="fn">${resume.basics.name}</h1>
        
            <p>
                ${resume.basics.location && resume.basics.location.city ? `
                <span class="icon fas fa-map-pin">
                    <span class="icon-text">${resume.basics.location.city}<span>
                </span>`
                    : ''}
                ${resume.basics.email ? `
                <a href="mailto:${resume.basics.email}">
                    <span class="icon fas fa-envelope">
                        <span class="icon-text">${resume.basics.email}</span>
                    </span>
                </a>` : ''}
            </p>

            <div id="profiles">
                <p>
                ${resume.basics.profiles
                    .map(profile => `
                    <a href="${profile.url}" target="_blank">
                        <span class="icon ${getIconClass(profile.network)}">
                            <span class="icon-text">${profile.username}</span>
                        </span>
                    </a>`)
                    .join(' ')}
                </p>
            </div>
        </div>
        
        ${resume.basics.summary ? `
        <div id="objective">
            <p>
                ${resume.basics.summary}
            </p>
        </div>` : ''}
        
        <div class="clear"></div>
        
        <dl>
            <dd class="clear"></dd>
            
            <dt>Erfarenhet</dt>
            ${resume.work
                .map(work => `
            <dd class="item">
                <h2>
                    ${work.name}, ${work.location}
                    <span>
                        ${work.startDate} -
                        ${work.endDate ? work.endDate : ''}
                    </span>
                </h2>
                <p>
                    <strong>${work.position}</strong>
                </p>
                ${work.summary ? `<p>${work.summary.replace('\\\\', '<br />').replace('<br /><br />', '</p><p>')}</p>` : ''}

                ${work.highlights && work.highlights.length > 0 ? `
                <ul>
                    ${work.highlights.map(highlight => `<li>${highlight}</li>`).join('')}
                </ul>
                ` : ''}
            </dd>`)
                .join('')}
            
            <dd class="clear"></dd>
            
            <dt>Utbildning</dt>
            ${resume.education
                .map(education => `
            <dd>
                <h2>
                    ${education.institution}
                    <span>
                        ${education.startDate} -
                        ${education.endDate ? education.endDate : ''}
                    </span>
                </h2>
                <p>
                    <strong>${education.studyType ? education.studyType + (education.area ? ', ' + education.area : '') : ''}</strong>
                </p>
            </dd>`)
                .join('')}
            
            <dd class="clear"></dd>
            
            <dt>Kompetenser</dt>
            <dd>
                ${resume.skills ? resume.skills.map(skills => `
                <h2>${skills.name}</h2>
                <p>
                    ${skills.keywords ? skills.keywords.join(', ') : ''}
                </p>`)
                .join('') : ''}
            </dd>
            
            <dd class="clear"></dd>
            
            <dt>Referenser</dt>
            <dd>
                <p>Referenser lämnas på begäran.</p>
            </dd>
            
            <dd class="clear"></dd>
        </dl>
        
        <div class="clear"></div>
    
    </div>

</body>

</html>
`