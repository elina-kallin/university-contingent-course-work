"""Print service for orders - генерация HTML для печати"""


def generate_enrollment_order_html(
    order: dict, 
    students: list, 
    university_name: str = "Университет",
    dean_full_name: str = "",
    faculty_name: str = "",
    faculty_short_name: str = ""
) -> str:
    """
    HTML для приказа о зачислении.
    
    Параметры:
    - order: dict с полями number, date, reason
    - students: list с полями last_name, name, patronymic, study_book_number, 
                group_name, faculty_name, direction_name, education_form, price
    - university_name: название университета
    - dean_full_name: ФИО декана (например, "Святов Кирилл Валерьевич")
    - faculty_name: полное название факультета
    - faculty_short_name: короткое название факультета
    """
    
    # Генерация строк таблицы
    rows = ""
    for i, s in enumerate(students, 1):
        faculty = s.get("faculty_name", faculty_name)
        direction = s.get("direction_name", "")
        group = s.get("group_name", "")
        education_form = s.get("education_form", "очная")
        price = s.get("price", "")
        
        # Форматирование формы обучения
        form_map = {
            "full-time": "очная",
            "part-time": "заочная",
            "extramural": "очно-заочная"
        }
        if education_form in form_map:
            education_form = form_map[education_form]
        
        # Форматирование цены
        price_text = f"{price} руб." if price else "бесплатно"
        
        rows += f"""<tr>
            <td>{i}</td>
            <td>{s.get('last_name','')} {s.get('name','')} {s.get('patronymic','')}</td>
            <td>{s.get('study_book_number','')}</td>
            <td>{faculty}</td>
            <td>{direction}</td>
            <td>{group}</td>
            <td>{education_form}</td>
            <td>{price_text}</td>
        </tr>"""
    
    # Текст от имени декана
    dean_text = ""
    if dean_full_name:
        if faculty_short_name:
            dean_text = f"от имени декана факультета {faculty_short_name} {dean_full_name}"
        elif faculty_name:
            dean_text = f"от имени декана {faculty_name} {dean_full_name}"
        else:
            dean_text = f"от имени декана {dean_full_name}"
    
    html = f"""<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Приказ о зачислении №{order.get('number','')}</title>
    <style>
        body {{
            font-family: "Times New Roman", serif;
            font-size: 14pt;
            line-height: 1.5;
            margin: 0;
            padding: 20px;
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .university-name {{
            font-size: 16pt;
            font-weight: bold;
            margin-bottom: 10px;
        }}
        .order-title {{
            font-size: 18pt;
            font-weight: bold;
            margin: 30px 0;
            text-align: center;
        }}
        .order-meta {{
            text-align: right;
            margin: 20px 0;
        }}
        .dean-text {{
            text-align: center;
            font-style: italic;
            margin: 20px 0;
            font-weight: bold;
        }}
        .reason-text {{
            margin: 20px 0;
            text-align: justify;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin: 30px 0;
            font-size: 12pt;
        }}
        th, td {{
            border: 1px solid #000;
            padding: 8px;
            text-align: left;
            vertical-align: top;
        }}
        th {{
            background-color: #f0f0f0;
            font-weight: bold;
            text-align: center;
        }}
        .signature {{
            margin-top: 50px;
            text-align: left;
        }}
        .signature-line {{
            margin-top: 30px;
        }}
        @media print {{
            @page {{
                margin: 2cm;
                size: A4;
            }}
            body {{
                font-size: 12pt;
                padding: 0;
            }}
        }}
    </style>
</head>
<body>
    <div class="header">
        <div class="university-name">{university_name}</div>
    </div>
    
    <div class="order-title">ПРИКАЗ</div>
    <div style="text-align: center; margin-bottom: 20px;">О зачислении студентов</div>
    
    <div class="order-meta">
        Дата: {order.get('date','')}<br>
        № {order.get('number','')}
    </div>
    
    {f'<div class="dean-text">{dean_text}</div>' if dean_text else ''}
    
    <div class="reason-text">
        <b>Приказываю зачислить следующих студентов:</b>
    </div>
    
    <table>
        <thead>
            <tr>
                <th style="width: 5%;">№</th>
                <th style="width: 25%;">ФИО студента</th>
                <th style="width: 10%;">№ дела</th>
                <th style="width: 15%;">Факультет</th>
                <th style="width: 15%;">Направление</th>
                <th style="width: 10%;">Группа</th>
                <th style="width: 10%;">Форма обучения</th>
                <th style="width: 10%;">Стоимость</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>
    
    <div class="reason-text">
        {order.get('reason','')}
    </div>
    
    <div class="signature">
        <div class="signature-line">
            _________________ / {dean_full_name if dean_full_name else '_________________'} /
        </div>
        <div style="font-size: 10pt; margin-top: 5px;">
            (подпись) (расшифровка подписи)
        </div>
    </div>
</body>
</html>"""
    
    return html


def generate_expulsion_order_html(
    order: dict, 
    students: list, 
    university_name: str = "Университет",
    dean_full_name: str = ""
) -> str:
    """HTML для приказа об отчислении"""
    rows = ""
    for i, s in enumerate(students, 1):
        rows += f"""<tr>
            <td>{i}</td>
            <td>{s.get('last_name','')} {s.get('name','')} {s.get('patronymic','')}</td>
            <td>{s.get('study_book_number','')}</td>
            <td>{s.get('group_name','')}</td>
            <td>{s.get('expulsion_reason','')}</td>
        </tr>"""
    
    html = f"""<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Приказ об отчислении №{order.get('number','')}</title>
    <style>
        body {{ font-family: "Times New Roman", serif; font-size: 14pt; line-height: 1.5; }}
        .header {{ text-align: center; margin-bottom: 30px; }}
        .order-title {{ font-size: 18pt; font-weight: bold; margin: 30px 0; text-align: center; }}
        .order-meta {{ text-align: right; margin: 20px 0; }}
        table {{ width: 100%; border-collapse: collapse; margin: 30px 0; }}
        th, td {{ border: 1px solid #000; padding: 8px; text-align: left; }}
        th {{ background-color: #f0f0f0; font-weight: bold; text-align: center; }}
        .signature {{ margin-top: 50px; }}
        @media print {{ @page {{ margin: 2cm; size: A4; }} }}
    </style>
</head>
<body>
    <div class="header">
        <div style="font-size: 16pt; font-weight: bold;">{university_name}</div>
    </div>
    
    <div class="order-title">ПРИКАЗ</div>
    <div style="text-align: center; margin-bottom: 20px;">Об отчислении студентов</div>
    
    <div class="order-meta">
        Дата: {order.get('date','')}<br>
        № {order.get('number','')}
    </div>
    
    {f'<div style="text-align: center; font-style: italic; margin: 20px 0;">от имени декана {dean_full_name}</div>' if dean_full_name else ''}
    
    <div style="margin: 20px 0;"><b>Приказываю отчислить следующих студентов:</b></div>
    
    <table>
        <thead>
            <tr>
                <th style="width: 5%;">№</th>
                <th style="width: 35%;">ФИО студента</th>
                <th style="width: 15%;">№ дела</th>
                <th style="width: 15%;">Группа</th>
                <th style="width: 30%;">Причина</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>
    
    <div style="margin: 20px 0;">{order.get('reason','')}</div>
    
    <div class="signature">
        <div style="margin-top: 30px;">
            _________________ / {dean_full_name if dean_full_name else '_________________'} /
        </div>
        <div style="font-size: 10pt; margin-top: 5px;">(подпись) (расшифровка подписи)</div>
    </div>
</body>
</html>"""
    
    return html


def generate_generic_order_html(
    order: dict, 
    students: list, 
    title: str, 
    university_name: str = "Университет",
    dean_full_name: str = ""
) -> str:
    """HTML для любого приказа"""
    rows = ""
    for i, s in enumerate(students, 1):
        rows += f"""<tr>
            <td>{i}</td>
            <td>{s.get('last_name','')} {s.get('name','')} {s.get('patronymic','')}</td>
            <td>{s.get('study_book_number','')}</td>
            <td>{s.get('group_name','')}</td>
        </tr>"""
    
    html = f"""<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>{title} №{order.get('number','')}</title>
    <style>
        body {{ font-family: "Times New Roman", serif; font-size: 14pt; line-height: 1.5; }}
        .header {{ text-align: center; margin-bottom: 30px; }}
        .order-title {{ font-size: 18pt; font-weight: bold; margin: 30px 0; text-align: center; }}
        .order-meta {{ text-align: right; margin: 20px 0; }}
        table {{ width: 100%; border-collapse: collapse; margin: 30px 0; }}
        th, td {{ border: 1px solid #000; padding: 8px; text-align: left; }}
        th {{ background-color: #f0f0f0; font-weight: bold; text-align: center; }}
        .signature {{ margin-top: 50px; }}
        @media print {{ @page {{ margin: 2cm; size: A4; }} }}
    </style>
</head>
<body>
    <div class="header">
        <div style="font-size: 16pt; font-weight: bold;">{university_name}</div>
    </div>
    
    <div class="order-title">{title}</div>
    
    <div class="order-meta">
        Дата: {order.get('date','')}<br>
        № {order.get('number','')}
    </div>
    
    {f'<div style="text-align: center; font-style: italic; margin: 20px 0;">от имени декана {dean_full_name}</div>' if dean_full_name else ''}
    
    <div style="margin: 20px 0;">{order.get('reason','')}</div>
    
    <table>
        <thead>
            <tr>
                <th style="width: 5%;">№</th>
                <th style="width: 40%;">ФИО студента</th>
                <th style="width: 20%;">№ дела</th>
                <th style="width: 35%;">Группа</th>
            </tr>
        </thead>
        <tbody>
            {rows}
        </tbody>
    </table>
    
    <div class="signature">
        <div style="margin-top: 30px;">
            _________________ / {dean_full_name if dean_full_name else '_________________'} /
        </div>
        <div style="font-size: 10pt; margin-top: 5px;">(подпись) (расшифровка подписи)</div>
    </div>
</body>
</html>"""
    
    return html
