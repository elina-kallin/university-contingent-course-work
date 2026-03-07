import os
from pathlib import Path


def collect_models_content():
    # Определяем пути
    current_dir = Path(__file__).parent if "__file__" in locals() else Path.cwd()
    models_dir = current_dir / "app" / "models"
    output_file = current_dir / "all_models.txt"

    # Проверяем существование папки models
    if not models_dir.exists():
        print(f"Ошибка: Папка {models_dir} не найдена!")
        return

    # Собираем все .py файлы
    py_files = list(models_dir.glob("*.py"))

    if not py_files:
        print(f"В папке {models_dir} не найдено .py файлов")
        return

    # Открываем файл для записи
    with open(output_file, "w", encoding="utf-8") as outfile:
        # Обрабатываем каждый .py файл
        for py_file in sorted(py_files):  # сортируем для консистентности
            try:
                # Читаем содержимое файла
                with open(py_file, "r", encoding="utf-8") as infile:
                    content = infile.read()

                # Записываем заголовок с именем файла
                outfile.write(f"{'='*80}\n")
                outfile.write(f"Файл: {py_file.name}\n")
                outfile.write(f"{'='*80}\n\n")

                # Записываем содержимое
                outfile.write(content)
                outfile.write("\n\n")

                print(f"Обработан файл: {py_file.name}")

            except Exception as e:
                error_msg = f"Ошибка при чтении файла {py_file.name}: {str(e)}"
                print(error_msg)
                outfile.write(f"{'='*80}\n")
                outfile.write(f"ОШИБКА в файле {py_file.name}: {str(e)}\n")
                outfile.write(f"{'='*80}\n\n")

    print(f"\nГотово! Содержимое сохранено в {output_file}")
    print(f"Обработано файлов: {len(py_files)}")


if __name__ == "__main__":
    collect_models_content()
