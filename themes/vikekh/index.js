function getIconCssClass(network) {
  switch (network.toLowerCase()) {
    case "github":
      return "fab fa-github";
    case "json resume":
      return "far fa-file";
    case "linkedin":
      return "fab fa-linkedin";
  }
}

exports.render = (resume) => `
<!doctype html>

<html>

<head>

<meta charset="UTF-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<!--<script src="https://cdn.jsdelivr.net/npm/@tailwindcss/browser@4"></script>-->
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/css/all.min.css" rel="stylesheet">
<link href="css/style.css" rel="stylesheet">

</head>

<body>

<div>

<h1>${resume.basics ? resume.basics.name : undefined}</h1>

${
  resume.basics &&
  ((resume.basics.location && resume.basics.location.city) ||
    resume.basics.email)
    ? `
<ul>
    ${
      resume.basics.location && resume.basics.location.city
        ? `
    <li>
        <i class="fas fa-map-pin"></i>
        ${resume.basics.location.city}
    </li>`
        : ""
    }
    ${
      resume.basics.email
        ? `
    <li>
        <a href="mailto:${resume.basics.email}">
            <i class="fas fa-envelope"></i>
            ${resume.basics.email}
        </a>
    </li>`
        : ""
    }
</ul>`
    : ""
}

${
  resume.basics && resume.basics.profiles && resume.basics.profiles.length > 0
    ? `
<ul>
    ${(resume.basics.profiles ?? [])
      .map(
        (profile) => `
    <li>
        ${
          profile.url
            ? `
        <a href="${profile.url}" target="_blank">`
            : ""
        }
            <i class="${getIconCssClass(profile.network)}"></i>
            ${profile.username}
        ${
          profile.url
            ? `
        </a>`
            : ""
        }
    </li>`
      )
      .join("")}
</ul>`
    : ""
}

${
  resume.work && resume.work.length > 0
    ? `
<h2>Erfarenhet</h2>

    ${resume.work
      .map(
        (work) => `
<h3>
    ${work.position},
    <span>${work.name}, ${work.location}</span>
    <span>${work.startDate} - ${work.endDate ? work.endDate : ""}</span>
</h3>

        ${
          work.summary
            ? `
<p>
    ${work.summary.replace("\\\\", "<br />").replace("<br /><br />", "</p><p>")}
</p>`
            : ""
        }
        
        ${
          work.highlights && work.highlights.length > 0
            ? `
<ul>
            ${work.highlights
              .map(
                (highlight) => `
    <li>${highlight}</li>`
              )
              .join("")}
</ul>`
            : ""
        }`
      )
      .join("")}`
    : ""
}

${
  resume.education && resume.education.length > 0
    ? `
<h2>Utbildning</h2>

    ${resume.education
      .map(
        (education) => `
<h3>
    ${education.studyType}${education.area ? `, ${education.area}` : ""}
    <span>${education.institution}</span>
    <span>${education.startDate} - ${
          education.endDate ? education.endDate : ""
        }</span>
</h3>`
      )
      .join("")}`
    : ""
}

${
  resume.skills && resume.skills.length > 0
    ? `
<h2>Kompetenser</h2>

    ${resume.skills
      .map(
        (skill) => `
<h3>
    ${skill.name}
</h3>

${
  skill.keywords
    ? `
<p>
    ${skill.keywords.join(", ")}
</p>`
    : ""
}`
      )
      .join("")}`
    : ""
}

</div>

</body>

</html>`;
