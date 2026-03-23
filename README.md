# DVLD - Drivers & Vehicles Licenses Department System

## 📝 Overview
The Drivers & Vehicles Licenses Department (DVLD) system is a robust desktop application built to automate and streamline the administrative processes of licensing. Developed as part of a specialized educational track under the mentorship of **Eng. Mohammed Abu-Hadhoud**, this project serves as a comprehensive solution for managing people, drivers, and various types of licenses with high technical precision.

## ✨ Key Features
* **Comprehensive People Management:** A centralized module to register, update, and manage personal data including National ID, contact info, and personal photos.
* **User & Security System:** A secure authentication layer to manage system users (employees) with the ability to activate or deactivate accounts.
* **Driver Management:** Seamlessly transition individuals into registered drivers.
* **License Lifecycle Tracking:**
    * Issuing and renewing local driving licenses.
    * Issuing and tracking International Driving Permits (IDP).
    * Comprehensive license history for every driver.
* **Advanced Data Filtering:** Dynamic search and filtering capabilities for all records using various criteria (ID, National No, Name).

## 🛠 Tech Stack
* **Programming Language:** C#
* **Framework:** .NET Framework
* **Database:** MS SQL Server
* **Data Access:** ADO.NET (Standard & Optimized)
* **Architecture:** Layered Architecture (N-Tier) to ensure separation of concerns.

## 🏗 Coding Principles & Philosophy
This project was developed with a strict focus on professional software engineering standards:
* **Divide and Conquer:** Breaking down complex business logic into small, manageable, and reusable methods.
* **Readability & Maintainability:** The code is written to be easily read and maintained by other developers.
* **Clarity Over Cleverness:** Avoiding overly "clever" or complex solutions if they compromise the clarity and simplicity of the code.
* **Standardized Naming:** Following consistent naming conventions for variables, methods, and classes.

---

## 🔧 Troubleshooting Common Issues

### 1. Database Restore Error

**Problem:**
* `DVLD.bak` file is missing or path is incorrect
* SQL Server is not running
* Insufficient permissions

**Solution:**
1. Make sure SQL Server is running (SQL Server Management Studio is open and connected)
2. Verify that `DVLD.bak` file exists in the path: `Desktop\DVLD\Database\DVLD.bak`
3. If restoring from a file, use the full path instead of a relative path
4. If you encounter permission issues, run SSMS as Administrator (Run as Administrator)
5. After restoring the Backup, run the following files in order:
   * `Desktop\DVLD\Database\Queries\Restore DB.sql`
   * `Desktop\DVLD\Database\Queries\Change Owner.sql`

---

### 2. "Login failed" Error

**Problem:**
* Login credentials are incorrect (username or password)
* Database was not restored successfully
* Authentication mode is incorrect

**Solution:**
1. Make sure the username is `guest` and password is `1234`
2. Verify that the `DVLD` database exists in SQL Server (check in Object Explorer)
3. If it doesn't exist, restore the backup again
4. Make sure SQL Server allows Mixed Mode Authentication
5. Verify that the files (`Restore DB.sql` and `Change Owner.sql`) were executed successfully

---

### 3. Query Execution Failed

**Problem:**
* Scripts (`Restore DB.sql` and `Change Owner.sql`) were not executed
* Execution order is incorrect
* Errors in the scripts themselves

**Solution:**
1. Open SSMS and verify connection to SQL Server
2. Copy the content of `Restore DB.sql` file from `Desktop\DVLD\Database\Queries`
3. Paste it in a new Query window in SSMS and click Execute
4. Then copy the content of `Change Owner.sql` file and paste it in a new Query window and click Execute
5. **Order matters:** `Restore DB.sql` first, then `Change Owner.sql`
6. Verify no error messages appear after each execution

---

### 4. Solution To: "Saving changes is not permitted" error

**Problem:**
* When you try to modify a table or change database structure, you get the message: "Saving changes is not permitted"
* This behavior exists in SSMS as a security feature

**Solution:**
1. Open SSMS
2. Click on **Tools** from the top menu
3. Select **Options**
4. Go to **Designers** (on the left side)
5. Select **Table and Database Designers**
6. Uncheck **Prevent saving changes that require table re-creation**
7. Click **OK**
8. Restart SSMS

**Note:** This only applies if you need to modify tables. Initially, you won't need this.

---

# نظام دائرة ترخيص السائقين والمركبات (DVLD)

## 📝 نبذة عن المشروع
نظام دائرة ترخيص السائقين والمركبات (DVLD) هو تطبيق مكتبي قوي تم بناؤه لأتمتة وتسهيل العمليات الإدارية الخاصة بالترخيص. تم تطويره كجزء من مسار تعليمي متخصص تحت إشراف **المهندس محمد أبو هدهود**، ويعد هذا المشروع حلاً شاملاً لإدارة الأشخاص والسائقين وأنواع الرخص المختلفة بدقة تقنية عالية.

## ✨ المميزات الرئيسية
* **إدارة شاملة للأشخاص:** وحدة مركزية لتسجيل وتحديث وإدارة البيانات الشخصية بما في ذلك الرقم الوطني، معلومات الاتصال، والصور الشخصية.
* **نظام المستخدمين والأمان:** طبقة حماية لإدارة مستخدمي النظام (الموظفين) مع إمكانية تفعيل أو تعطيل الحسابات.
* **إدارة السائقين:** تحويل الأفراد بسلاسة إلى سائقين مسجلين في النظام.
* **تتبع دورة حياة الرخص:**
    * إصدار وتجديد رخص القيادة المحلية.
    * إصدار وتتبع رخص القيادة الدولية (IDP).
    * سجل كامل وتاريخي للرخص لكل سائق.
* **تصفية متقدمة للبيانات:** إمكانيات بحث وتصفية ديناميكية لجميع السجلات باستخدام معايير مختلفة (المعرف، الرقم الوطني، الاسم).

## 🛠 التقنيات المستخدمة
* **لغة البرمجة:** C#
* **بيئة العمل:** .NET Framework
* **قاعدة البيانات:** MS SQL Server
* **الوصول للبيانات:** ADO.NET (بطريقة معيارية ومحسنة)
* **المعمارية:** معمارية الطبقات (N-Tier Architecture) لضمان فصل المهام.

## 🏗 مبادئ وفلسفة البرمجة
تم تطوير هذا المشروع مع التركيز الصارم على معايير هندسة البرمجيات الاحترافية:
* **فرق تسد:** تقسيم منطق الأعمال المعقد إلى دالّات (Methods) صغيرة، قابلة للإدارة وإعادة الاستخدام.
* **قابلية القراءة والصيانة:** الكود مكتوب بطريقة تسهل قراءته وصيانته من قبل مطورين آخرين.
* **الوضوح مقدم على التعقيد:** تجنب الحلول "الذكية" المبالغ فيها إذا كانت تؤثر على وضوح وبساطة الكود.
* **تسمية معيارية:** اتباع اتفاقيات تسمية متسقة للمتغيرات والدالّات والأصناف (Classes).

---

**إشراف:** م. محمد أبو هدهود

---

## 🔧 حل المشاكل الشائعة

### 1. خطأ في استعادة قاعدة البيانات

**المشكلة:**
* ملف `DVLD.bak` مش موجود في المسار أو المسار غلط
* SQL Server مش مشغل
* صلاحيات ناقصة

**الحل:**
1. تأكد من أن SQL Server شغّال (SQL Server Management Studio مفتوح ومتصل)
2. تأكد من وجود ملف `DVLD.bak` في المسار: `Desktop\DVLD\Database\DVLD.bak`
3. إذا كنت تستعيد من ملف موجود، استخدم الـ path الكامل وليس relative path
4. إذا حصلت مشكلة في الصلاحيات، شغّل SSMS كمسؤول (Run as Administrator)
5. بعد استعادة الـ Backup، شغّل الملفات التالية بالترتيب:
   * `Desktop\DVLD\Database\Queries\Restore DB.sql`
   * `Desktop\DVLD\Database\Queries\Change Owner.sql`

---

### 2. خطأ "Login failed"

**المشكلة:**
* بيانات تسجيل الدخول غلط (اسم المستخدم أو كلمة السر)
* قاعدة البيانات ما استعادت بنجاح
* Authentication mode غلط

**الحل:**
1. تأكد من أن اسم المستخدم هو `guest` وكلمة السر `1234`
2. تحقق من أن قاعدة البيانات `DVLD` موجودة في SQL Server (شوفها في Object Explorer)
3. إذا ما فيها، استعد الـ backup من جديد
4. تأكد من أن SQL Server مسموح فيه Mixed Mode Authentication
5. تأكد من تنفيذ الملفات (`Restore DB.sql` و `Change Owner.sql`) بنجاح

---

### 3. فشل تشغيل الـ Queries

**المشكلة:**
* Scripts (`Restore DB.sql` و `Change Owner.sql`) ما تمت تنفيذها
* ترتيب التنفيذ غلط
* أخطاء في الـ Scripts نفسها

**الحل:**
1. افتح SSMS وتأكد من الاتصال بـ SQL Server
2. انسخ محتوى ملف `Restore DB.sql` من `Desktop\DVLD\Database\Queries`
3. الصقه في نافذة Query جديدة في SSMS واضغط Execute
4. بعدين انسخ محتوى ملف `Change Owner.sql` والصقه في نافذة جديدة واضغط Execute
5. **الترتيب مهم:** `Restore DB.sql` أولاً ثم `Change Owner.sql`
6. تأكد من عدم وجود رسائل خطأ بعد كل تنفيذ

---

### 4. Solution To: "Saving changes is not permitted" error

**المشكلة:**
* لما تحاول تعديل جدول أو تغيير structure قاعدة البيانات، بتطلع رسالة: "Saving changes is not permitted"
* هذا السلوك موجود في SSMS كخاصية أمان

**الحل:**
1. افتح SSMS
2. اضغط على **Tools** من القائمة العلوية
3. اختر **Options**
4. اذهب إلى **Designers** (في الجانب الأيسر)
5. اختر **Table and Database Designers**
6. شيل العلامة من **Prevent saving changes that require table re-creation**
7. اضغط **OK**
8. أعد تشغيل SSMS

**ملاحظة:** هذا يخصك فقط لو كنت محتاج تعديل الجداول. في البداية ما راح تحتاج هذا.
