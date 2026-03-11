"""Print service for orders - генерация HTML для печати"""

def generate_enrollment_order_html(order: dict, students: list, university_name: str = "Университет") -> str:
    """HTML для приказа о зачислении"""
    rows = ""
    for i, s in enumerate(students, 1):
        rows += f'<tr><td>{i}</td><td>{s.get("last_name","")} {s.get("name","")} {s.get("patronymic","")}</td><td>{s.get("study_book_number","")}</td><td>{s.get("group_name","")}</td></tr>'
    
    return f'''<!DOCTYPE html><html><head><meta charset="UTF-8"><title>Приказ №{order.get("number","")}</title>
<style>body{{font-family:"Times New Roman",serif;font-size:14pt}}table{{width:100%;border-collapse:collapse}}th,td{{border:1px solid #000;padding:8px}}.header{{text-align:center}}@media print{{@page{{margin:2cm}}}}</style>
</head><body><div class="header"><h2>{university_name}</h2></div>
<h3 style="text-align:center">ПРИКАЗ О зачислении</h3>
<p>Дата: {order.get("date","")} № {order.get("number","")}</p>
<p>{order.get("reason","")}</p>
<table><thead><tr><th>№</th><th>ФИО</th><th>№ дела</th><th>Группа</th></tr></thead><tbody>{rows}</tbody></table>
<p style="margin-top:50px">_________________ / _________________ / (подпись)</p>
</body></html>'''

def generate_expulsion_order_html(order: dict, students: list, university_name: str = "Университет") -> str:
    """HTML для приказа об отчислении"""
    rows = ""
    for i, s in enumerate(students, 1):
        rows += f'<tr><td>{i}</td><td>{s.get("last_name","")} {s.get("name","")} {s.get("patronymic","")}</td><td>{s.get("study_book_number","")}</td><td>{s.get("group_name","")}</td></tr>'
    
    return f'''<!DOCTYPE html><html><head><meta charset="UTF-8"><title>Приказ №{order.get("number","")}</title>
<style>body{{font-family:"Times New Roman",serif;font-size:14pt}}table{{width:100%;border-collapse:collapse}}th,td{{border:1px solid #000;padding:8px}}.header{{text-align:center}}@media print{{@page{{margin:2cm}}}}</style>
</head><body><div class="header"><h2>{university_name}</h2></div>
<h3 style="text-align:center">ПРИКАЗ Об отчислении</h3>
<p>Дата: {order.get("date","")} № {order.get("number","")}</p>
<p>{order.get("reason","")}</p>
<table><thead><tr><th>№</th><th>ФИО</th><th>№ дела</th><th>Группа</th></tr></thead><tbody>{rows}</tbody></table>
<p style="margin-top:50px">_________________ / _________________ / (подпись)</p>
</body></html>'''

def generate_generic_order_html(order: dict, students: list, title: str, university_name: str = "Университет") -> str:
    """HTML для любого приказа"""
    rows = ""
    for i, s in enumerate(students, 1):
        rows += f'<tr><td>{i}</td><td>{s.get("last_name","")} {s.get("name","")} {s.get("patronymic","")}</td><td>{s.get("study_book_number","")}</td><td>{s.get("group_name","")}</td></tr>'
    
    return f'''<!DOCTYPE html><html><head><meta charset="UTF-8"><title>{title} №{order.get("number","")}</title>
<style>body{{font-family:"Times New Roman",serif;font-size:14pt}}table{{width:100%;border-collapse:collapse}}th,td{{border:1px solid #000;padding:8px}}.header{{text-align:center}}@media print{{@page{{margin:2cm}}}}</style>
</head><body><div class="header"><h2>{university_name}</h2></div>
<h3 style="text-align:center">{title}</h3>
<p>Дата: {order.get("date","")} № {order.get("number","")}</p>
<p>{order.get("reason","")}</p>
<table><thead><tr><th>№</th><th>ФИО</th><th>№ дела</th><th>Группа</th></tr></thead><tbody>{rows}</tbody></table>
<p style="margin-top:50px">_________________ / _________________ / (подпись)</p>
</body></html>'''
