using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    static class ControlColor
    {
        // テキストエディターの背景色
        public static Color editorBackground = Color.FromArgb(30, 30, 30);

        // テキストエディターの文字色
        public static Color editorText = Color.FromArgb(212, 212, 212);

        // テキストエディターでの選択範囲の背景色
        public static Color editorSelectionBackground = Color.FromArgb(38, 79, 120);

        // コメントの文字色
        public static Color commentText = Color.FromArgb(106, 153, 85);

        // 文字列リテラルの文字色
        public static Color stringLiteralText = Color.FromArgb(206, 145, 120);

        // キーワードの文字色
        public static Color keywordText = Color.FromArgb(78, 201, 176);

        // 数値リテラルの文字色
        public static Color numericLiteralText = Color.FromArgb(181, 206, 168);

        // メソッド名の文字色
        public static Color methodNameText = Color.FromArgb(156, 220, 254);

        // クラス名の文字色
        public static Color classNameText = Color.FromArgb(78, 201, 176);

        // インターフェイス名の文字色
        public static Color interfaceNameText = Color.FromArgb(156, 220, 254);

        // 変数の文字色
        public static Color variableText = Color.FromArgb(215, 186, 125);

        // 変数型の文字色
        public static Color variableTypeText = Color.FromArgb(86, 156, 214);
    }
}
